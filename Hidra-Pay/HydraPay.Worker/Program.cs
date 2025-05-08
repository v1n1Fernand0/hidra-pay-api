using HidraPay.Messaging.Configuration;
using HidraPay.Worker.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddMessaging(ctx.Configuration);

        services.AddHostedService<PaymentEventsWorker>();
    })
    .Build()
    .Run();
