using MassTransit;
using Messaging.Model;

namespace Message;

public class TestConsumer : IConsumer<TestMessage>
{
    public Task Consume(ConsumeContext<TestMessage> context)
    {
        var message = context.Message;
        return Console.Out.WriteLineAsync($"Received: {context.Message.Text}");
    }
}