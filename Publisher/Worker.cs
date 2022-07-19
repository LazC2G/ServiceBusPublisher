using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace EventBus
{
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string? Summary { get; set; }
    }

    public class Worker : BackgroundService
    {
        readonly IBus _bus;

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new Message { Text = $"The time is {DateTimeOffset.Now}" }, stoppingToken);
                Console.WriteLine("Published Text {0}", DateTimeOffset.Now);

                //var weatherForecast = new WeatherForecast
                //{
                //    Date = DateTime.Parse("2022-08-01"),
                //    TemperatureCelsius = 28,
                //    Summary = "Hot"
                //};

                //await _bus.Publish(new JSON { Text = JsonSerializer.Serialize(weatherForecast) }, stoppingToken);
                //Console.WriteLine("Published Text: JSON");

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}