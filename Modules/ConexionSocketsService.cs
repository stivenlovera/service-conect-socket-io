


using System.Reactive.Linq;
using Socket.Io.Client.Core;
using Socket.Io.Client.Core.Model;

namespace service_rally_diciembre_2023.Modules
{
    public class ConexionSocketsService
    {
        private readonly ILogger<ConexionSocketsService> logger;

        public ConexionSocketsService(
          ILogger<ConexionSocketsService> logger
        )
        {
            this.logger = logger;
        }
        public async Task<string> Connexion()
        {
            using var client = new SocketIoClient();
            var options = new SocketIoOpenOptions("custom-path");
            //in this example we throttle event messages to 1 second
            var someEventSubscription = client.On("response")
                .Throttle(TimeSpan.FromSeconds(1)) //optional
                .Subscribe(message =>
            {
                Console.WriteLine($"Received event: {message.EventName}. Data: ");
            });

            await client.OpenAsync(new Uri("ws://localhost:3000"), options);

            //optionally unsubscribe (equivalent to off() from socket.io)
            someEventSubscription.Dispose();
            return "";
        }
    }
}