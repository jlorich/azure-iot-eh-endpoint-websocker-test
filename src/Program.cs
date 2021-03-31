using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoTEventHubEndpointTest
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var services = BuildServiceProvider();

            var producer = services.GetService<Producer>();
            var consumer = services.GetService<Consumer>();
            
            var cancellationSource = new CancellationTokenSource();
            cancellationSource.CancelAfter(TimeSpan.FromSeconds(60));

            var pt = producer.Start(cancellationSource.Token);
            var ct = consumer.Start(cancellationSource.Token);

            await Task.WhenAll(new Task[] {pt, ct});
        }

        public static ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();
            
            services.AddOptions();
            services.Configure<IoTEventHubEndpointTestOptions>(configuration);

            services.AddTransient<Producer>();
            services.AddTransient<Consumer>();

            return services.BuildServiceProvider();
        }
    }
}
