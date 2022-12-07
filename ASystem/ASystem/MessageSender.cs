using ASystem.RandomMessageGeneration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ASystem;

public class MessageSender : BackgroundService
{
    private readonly MessageGenerator _generator;
    private readonly ISystemClient _client;
    private readonly IOptions<MessageGenerationSettings> _aSystemSettings;
    private readonly Random _rand = new();

    public MessageSender(MessageGenerator generator, IOptions<MessageGenerationSettings> aSystemSettings, ISystemClient client)
    {
        _generator = generator;
        _aSystemSettings = aSystemSettings;
        _client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var maxMessageDelay = _aSystemSettings.Value.MaxMessageDelay;
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = _generator.Generate();
            await _client.SendMessageAsync(message);

            var delay = _rand.Next(maxMessageDelay);
            await Task.Delay(delay, stoppingToken);
        }
    }
}