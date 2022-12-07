using BSystem;
using BSystem.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace BSystemTests;

public class MessageControllerTest
{
    private const string MessageString = "testString";
    private const int Priority = 1;

    private MessageController _controller;

    private readonly Mock<ILogger<MessageController>> _loggerMock = new();
    private readonly Mock<IMessageQueue> _messageQueueMock = new();

    [SetUp]
    public void Setup()
    {
        _controller = new MessageController(_loggerMock.Object, _messageQueueMock.Object);
    }

    [Test]
    public void Enqueue_Received_Message()
    {
        var message = new PrioritizedMessage(MessageString, Priority);

        _controller.Post(message);

        _messageQueueMock.Verify(q => q.Enqueue(message));
    }
}