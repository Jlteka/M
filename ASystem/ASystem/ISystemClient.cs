using ASystem.RandomMessageGeneration;

namespace ASystem;

public interface ISystemClient
{
    public Task SendMessageAsync(PrioritizedMessage message);
}