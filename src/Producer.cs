using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Options;

namespace IoTEventHubEndpointTest
{
    public class Producer
    {
        private IoTEventHubEndpointTestOptions _Options;

        public Producer(IOptions<IoTEventHubEndpointTestOptions> options) {
            _Options = options.Value;
        }

        public async Task Start(CancellationToken cancellationToken) {

            var deviceAuthentication = new DeviceAuthenticationWithRegistrySymmetricKey(_Options.IoTHubDeviceId, _Options.IoTHubDeviceKey);

            DeviceClient deviceClient = DeviceClient.Create(_Options.IoTHubHostName, deviceAuthentication, TransportType.Mqtt);

            while (!cancellationToken.IsCancellationRequested) { 
                var dateString = DateTime.UtcNow.ToString("o");
                var messageString = $"{{\"date\": \"{dateString}\"}}";
                Message message = new Message(Encoding.ASCII.GetBytes(messageString));
                await deviceClient.SendEventAsync(message);
                Console.WriteLine("Message Sent:" + messageString);

                await Task.Delay(1000);
            }
        }
    }
}