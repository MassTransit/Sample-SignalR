using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleSignalR.ConsoleClient
{
    public class ChatHubConnectionHostedService :
        IHostedService
    {
        readonly ILogger _logger;
        readonly HubConnection _connection;

        public ChatHubConnectionHostedService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ChatHubConnectionHostedService>();

            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5100/chat")
                .WithAutomaticReconnect()
                .Build();

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _connection.On<string, string>("broadcastMessage", (name, message) =>
            {
                var newMessage = $"{name}: {message}";
                _logger.LogInformation(newMessage);
            });

            _logger.LogInformation("Starting ChatHub");
            await _connection.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping ChatHub");
            return _connection.StopAsync(cancellationToken);
        }
    }
}