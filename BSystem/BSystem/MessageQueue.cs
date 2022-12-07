namespace BSystem;

public class MessageQueue : IMessageQueue, IObservable<object>
{
    private readonly ILogger<MessageQueue> _logger;

    private static readonly PriorityQueue<string, int> Messages = new();
    private readonly List<IObserver<object>> _observers = new();

    public MessageQueue(ILogger<MessageQueue> logger)
    {
        _logger = logger;
    }

    public void Enqueue(PrioritizedMessage message)
    {
        if (string.IsNullOrEmpty(message.MessageString))
        {
            throw new ArgumentException("The MessageString field shouldn't be null or empty", nameof(message));
        }
        Messages.Enqueue(message.MessageString, message.Priority);

        _logger.LogTrace(message: $"Message: {message.MessageString} Priority: {message.Priority} enqueued");

        foreach (var observer in _observers)
        {
            observer.OnNext(new object());
        }
    }

    public bool TryDequeue(out PrioritizedMessage message)
    {
        var res = Messages.TryDequeue(out var messageString, out var priority);
        message = res
            ? new PrioritizedMessage(messageString, priority)
            : null;

        if (message != null)
        {
            _logger.LogTrace($"Message: {message.MessageString} Priority: {message.Priority} dequeued");
        }

        return res;

    }

    public IDisposable Subscribe(IObserver<object> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return new Unsubscriber(_observers, observer);
    }

    private class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<object>>_observers;
        private readonly IObserver<object> _observer;

        public Unsubscriber(List<IObserver<object>> observers, IObserver<object> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}