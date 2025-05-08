using HidraPay.Domain.Ports;
using HidraPay.Infrastructure.Gateways;
using HidraPay.Infrastructure.Gateways.AbacatePay;
using HidraPay.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using Stripe;

namespace HidraPay.Infrastructure.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            #region Stripe
            services.Configure<StripeSettings>(config.GetSection("Stripe"));
            services.AddSingleton(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<StripeSettings>>().Value;
                StripeConfiguration.ApiKey = settings.ApiKey;
                return new PaymentIntentService();
            });
            services.AddTransient<StripeGateway>();
            services.AddTransient<IPaymentGateway, StripeGateway>();
            #endregion

            #region AbacatePay
            services.Configure<AbacatePaySettings>(config.GetSection("AbacatePay"));
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));
            var circuitBreaker = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

            services.AddHttpClient<AbacatePayGateway>((sp, client) =>
            {
                var settings = sp.GetRequiredService<IOptions<AbacatePaySettings>>().Value;
                client.BaseAddress = new Uri(settings.BaseUrl);
                client.DefaultRequestHeaders.Add("X-API-KEY", settings.ApiKey);
                client.DefaultRequestHeaders.Add("X-API-SECRET", settings.ApiSecret);
            })
            .AddPolicyHandler(retryPolicy)
            .AddPolicyHandler(circuitBreaker)
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(10));

            services.AddTransient<AbacatePayGateway>();
            services.AddTransient<IPaymentGateway, AbacatePayGateway>();
            #endregion


            services.AddDbContext<HidraPayDbContext>(opts =>
                opts.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            services.AddTransient<IPaymentRepository, PaymentRepository>();

            return services;
        }
    }
}
