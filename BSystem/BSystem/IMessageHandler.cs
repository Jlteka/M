namespace BSystem;

public interface IMessageHandler
{
    public void Handle(PrioritizedMessage message);
}