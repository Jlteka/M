using Microsoft.AspNetCore.Mvc;

namespace BSystem.Controllers;

[ApiController]
[Route("message")]
public class MessageController : ControllerBase
{
    private readonly ILogger<MessageController> _logger;
    private readonly IMessageQueue _messageQueue;

    public MessageController(ILogger<MessageController> logger, IMessageQueue messageQueue)
    {
        _logger = logger;
        _messageQueue = messageQueue;
    }

    [HttpPost(Name = "PostMessage")]
    public IActionResult Post([FromBody] PrioritizedMessage query)
    {
        _logger.LogTrace($"Message: {query.MessageString} Priority: {query.Priority} received");
        _messageQueue.Enqueue(query);
        return Accepted();
    }
}