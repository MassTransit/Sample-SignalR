// See https://aka.ms/new-console-template for more information
using MassTransit;
using SampleSignalR.Contracts;

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host(new Uri("rabbitmq://localhost"), h =>
    {
        h.Username("guest");
        h.Password("guest");
    });
});

// Important! The bus must be started before using it!
await busControl.StartAsync();

do
{
    Console.WriteLine("Enter publish message (or quit to exit)");
    Console.Write("> ");
    var value = Console.ReadLine();

    if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
        break;

    await busControl.Publish(new BroadcastMessage("backend-process-publish", value));
}
while (true);

await busControl.StopAsync();