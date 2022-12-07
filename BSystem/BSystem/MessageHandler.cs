namespace BSystem;

public class MessageHandler : IMessageHandler
{
    private readonly ILogger<MessageHandler> _logger;

    public MessageHandler(ILogger<MessageHandler> logger)
    {
        _logger = logger;
    }

    public void Handle(PrioritizedMessage message)
    {
        Thread.Sleep(1000);
        _logger.LogTrace($"Message: {message!.MessageString} Priority: {message.Priority} processing completed");
    }
}