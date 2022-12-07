namespace BSystem;

public interface IMessageQueue
{
    public void Enqueue(PrioritizedMessage message);
    public bool TryDequeue(out PrioritizedMessage message);

}