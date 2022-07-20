using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;
using System;
using System.Threading.Tasks;


namespace EventBus
{
    public record Notification
    {
        public string Text { get; set; }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
               .ConfigureServices((services) =>
               {
                   services.AddMassTransit(x =>
                   {
                       x.UsingAzureServiceBus((context, cfg) =>
                       {
                           cfg.Host("Endpoint=sb://");
                           cfg.ConfigureEndpoints(context);
                       });
                   });

                   services.AddHostedService<Worker>();
               }).Build().RunAsync();
        }
    }
}
