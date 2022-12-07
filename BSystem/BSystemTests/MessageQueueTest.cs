using BSystem;
using Microsoft.Extensions.Logging;
using Moq;

namespace BSystemTests;

public class MessageQueueTest
{
    private const int Priority = 1;
    private const int LowestPriority = 0;
    private const string MessageString = "testString";

    private MessageQueue _queue;

    private readonly PrioritizedMessage _message = new(MessageString, Priority);
    private readonly Mock<ILogger<MessageQueue>> _loggerMock = new();

    [SetUp]
    public void Setup()
    {
        _queue = new MessageQueue(_loggerMock.Object);
    }

    [Test]
    public void Notify_Sub_Within_Message_Enqueue()
    {
        var subMock = new Mock<IObserver<object>>();
        _queue.Subscribe(subMock.Object);

        _queue.Enqueue(_message);

        subMock.Verify(x=>x.OnNext(It.IsAny<object>()));
    }

    [Test]
    public void Dequeue_Enqueued_Message()
    {
        _queue.Enqueue(_message);

        _queue.TryDequeue(out var dequeuedMessage);

        Assert.That(dequeuedMessage!.Priority == _message.Priority &&
                    dequeuedMessage.MessageString == _message.MessageString);
    }

    [Test]
    public void Dequeue_Lowest_Priority_Message()
    {
        _queue.Enqueue(new PrioritizedMessage(MessageString, Priority));
        _queue.Enqueue(new PrioritizedMessage(MessageString, LowestPriority));
        _queue.Enqueue(new PrioritizedMessage(MessageString, Priority));

        _queue.TryDequeue(out var dequeuedMessage);

        Assert.NotNull(dequeuedMessage);
        Assert.That(dequeuedMessage.Priority.Equals(LowestPriority));
    }
}