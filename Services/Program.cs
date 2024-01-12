using MassTransit;
using Message;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(AddRabbitMQ)
    .Build();

host.Run();


static void AddRabbitMQ(IServiceCollection services)
{
    services.AddMassTransit(x =>
    {
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host("amqp://guest:guest@10.90.10.221");

            cfg.Message<TestMessage>(
                        x => x.SetEntityName(nameof(TestMessage)));

            cfg.Publish<TestMessage>(
            x => x.ExchangeType = ExchangeType.Direct);

            cfg.ReceiveEndpoint("service-test-queue", e =>
            {
                e.Durable = true;
                e.SetQueueArgument("x-message-deduplication", true);
                e.Handler<TestMessage>(async context =>
                {
                    await Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
                });
            });

            cfg.ConfigureEndpoints(context);
        });
    });
}