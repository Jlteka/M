using BSystem;
using Moq;

namespace BSystemTests;

public interface IObservableQueue : IObservable<object>, IMessageQueue { }

public class MessageServiceTest
{
    private const int Priority = 1;
    private const string MessageString = "testString";

    private MessageService _service;

    private readonly Mock<IObservableQueue> _queueMock = new();
    private readonly Mock<IMessageHandler> _messageHandlerMock = new();

    [SetUp]
    public void Setup()
    {
        _service = new MessageService(_queueMock.Object,
            _messageHandlerMock.Object,
            _queueMock.Object);
    }

    [Test]
    public void MessageService_Subscribe_Queue()
    {
        var service = new MessageService(_queueMock.Object,
            _messageHandlerMock.Object,
            _queueMock.Object);

        _queueMock.Verify(x => x.Subscribe(It.IsAny<IObserver<object>>()));
    }

    [Test]
    public void MessageService_Process_All_Queued_Messages()
    {
        var prioritizedMessage = new PrioritizedMessage(MessageString, Priority);
        _queueMock.SetupSequence(x => x.TryDequeue(out prioritizedMessage))
            .Returns(true)
            .Returns(true)
            .Returns(false);

        _service.OnNext(new object());

        _messageHandlerMock.Verify(x => x.Handle(It.IsAny<PrioritizedMessage>()), Times.Exactly(2));
    }
}