using Microsoft.AspNetCore.Http.Connections;
using SignalR.Extensions;
using SignalR.Hubs;
using SignalR.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var policyName = "defaultCorsPolicy";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(policyName, builder =>
            {
                builder.WithOrigins("https://localhost:4200") // the Angular app url
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    ;
            });
        });

       
        builder.Services.AddSignalR();

        var app = builder.Build();

        app.MapGet("/", () => "Hub is working!");

        app.UseCors(policyName);
        app.UseExceptionMiddleware();

        app.MapHub<ChatHub>("/chathub", options =>
        {
            options.Transports =
                HttpTransportType.WebSockets |
                HttpTransportType.LongPolling;
        }
        );

        app.Run();
    }
}