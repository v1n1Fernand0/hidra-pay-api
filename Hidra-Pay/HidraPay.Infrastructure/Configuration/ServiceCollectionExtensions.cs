using System;
using System.Net.Http;
using System.Net.Http.Json;
using HidraPay.Domain.Ports;
using HidraPay.Infrastructure.Configuration;
using HidraPay.Infrastructure.Gateways;
using HidraPay.Infrastructure.Gateways.AbacatePay;
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
            //
            // 1) Stripe SDK (apenas CreditCard)
            //
            services.Configure<StripeSettings>(config.GetSection("Stripe"));

            services.AddSingleton(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<StripeSettings>>().Value;
                StripeConfiguration.ApiKey = settings.ApiKey;
                return new PaymentIntentService();
            });
            services.AddTransient<StripeGateway>();
            services.AddTransient<IPaymentGateway, StripeGateway>();

            //
            // 2) AbacatePay via HttpClient + Polly (Pix e Boleto)
            //
            services.Configure<AbacatePaySettings>(config.GetSection("AbacatePay"));

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))
                );

            var circuitBreaker = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30)
                );

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

            return services;
        }
    }
}
