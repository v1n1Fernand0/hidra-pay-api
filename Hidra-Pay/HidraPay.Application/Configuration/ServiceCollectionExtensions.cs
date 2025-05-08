using HidraPay.Application.Services;
using HidraPay.Application.UseCases.AuthorizePayment;
using HidraPay.Application.UseCases.CapturePayment;
using HidraPay.Application.UseCases.GetStatus;
using HidraPay.Application.UseCases.RefundPayment;
using Microsoft.Extensions.DependencyInjection;

namespace HidraPay.Application.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<PaymentGatewayFactory>();
            services.AddTransient<PaymentService>();

            services.AddTransient<IAuthorizePaymentUseCase, AuthorizePaymentUseCase>();
            services.AddTransient<ICapturePaymentUseCase, CapturePaymentUseCase>();
            services.AddTransient<IRefundPaymentUseCase, RefundPaymentUseCase>();
            services.AddTransient<IGetStatusUseCase, GetStatusUseCase>();

            return services;
        }
    }
}
