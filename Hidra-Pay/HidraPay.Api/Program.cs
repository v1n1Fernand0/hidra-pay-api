using HidraPay.Application.Configuration;
using HidraPay.Domain.Configuration;
using HidraPay.Infrastructure.Configuration;
using HidraPay.Messaging.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddMessaging(builder.Configuration);

builder.Services      
    .AddOpenApi()
    .AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();                         
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
