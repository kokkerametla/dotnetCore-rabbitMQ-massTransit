using MassTransit;
using Message;
using Messaging.Model;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(AddRabbitMQ)
    .Build();

host.Run();


static void AddRabbitMQ(IServiceCollection services)
{
    services.AddMassTransit(x =>
    {

        x.AddConsumer<TestConsumer>();
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host("amqp://guest:guest@localhost");

            cfg.Message<TestMessage>(
                        x => x.SetEntityName(nameof(TestMessage)));

            cfg.Publish<TestMessage>(
            x =>
            {
                x.ExchangeType = ExchangeType.Direct;
            });

            cfg.ReceiveEndpoint("service-test-queue", e =>
            {
                e.PrefetchCount = 2;
                e.ConfigureConsumeTopology = false;
                e.ClearSerialization();
                e.UseRawJsonSerializer();
                e.SetQueueArgument("x-message-deduplication", true);

                e.ConfigureConsumer<TestConsumer>(context);

                e.Bind(nameof(TestMessage), x =>
                {
                    x.ExchangeType = ExchangeType.Direct;
                });
            });


            cfg.ConfigureEndpoints(context);
        });
    });
}