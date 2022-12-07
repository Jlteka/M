using Microsoft.Extensions.Options;

namespace ASystem.RandomMessageGeneration;

public class MessageGenerator
{
    private readonly IOptions<MessageGenerationSettings> _aSystemSettings;
    private readonly IMessageStringGenerator _messageStringGenerator;

    public MessageGenerator(IMessageStringGenerator messageStringGenerator, IOptions<MessageGenerationSettings> aSystemSettings)
    {
        _messageStringGenerator = messageStringGenerator;
        _aSystemSettings = aSystemSettings;
    }

    public PrioritizedMessage Generate()
    {
        Random rand = new();
        int maxPriority = _aSystemSettings.Value.MaxPriority;
        int priority = rand.Next(maxPriority + 1);
        string messageString = _messageStringGenerator.Generate();

        return new PrioritizedMessage(messageString, priority);
    }
}