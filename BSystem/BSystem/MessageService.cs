namespace BSystem;

public class MessageService : BackgroundService, IObserver<object>
{
    private readonly IMessageQueue _messageQueue;
    private readonly IMessageHandler _messageHandler;

    private Task _eTask = Task.CompletedTask;

    public MessageService(IMessageQueue messageQueue,
                          IMessageHandler messageHandler,
                          IObservable<object> observable)
    {
        _messageQueue = messageQueue;
        _messageHandler = messageHandler;

        observable.Subscribe(this);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Execute(stoppingToken);

        return Task.CompletedTask;
    }

    private void Execute(CancellationToken stoppingToken)
    {
        _eTask = Task.Factory.StartNew(() =>
        {
            while (!stoppingToken.IsCancellationRequested && _messageQueue.TryDequeue(out var message))
            {
                if (message != null)
                {
                    _messageHandler.Handle(message);
                }
            }
        }, stoppingToken);
    }

    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public void OnNext(object value)
    {
        if (_eTask.IsCompleted)
        {
            Execute(new CancellationToken());
        }
    }
}