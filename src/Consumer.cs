using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Microsoft.Extensions.Options;

namespace IoTEventHubEndpointTest
{
    public class Consumer
    {
        private IoTEventHubEndpointTestOptions _Options;

        public Consumer(IOptions<IoTEventHubEndpointTestOptions> options) {
            _Options = options.Value;
        }

        public async Task Start(CancellationToken cancellationToken) {
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            var clientOptions = new EventHubConsumerClientOptions {
                ConnectionOptions = new EventHubConnectionOptions {
                    TransportType = EventHubsTransportType.AmqpWebSockets
                }
            };

            await using (var consumer = new EventHubConsumerClient(consumerGroup, _Options.EventHubConnectionString, _Options.EventHubName, clientOptions))
            {
                await foreach (PartitionEvent receivedEvent in consumer.ReadEventsAsync(cancellationToken))
                {
                    Console.Out.WriteLine("Event recieved: " + Encoding.UTF8.GetString(receivedEvent.Data.EventBody));
                }
            }
        }
    }
}