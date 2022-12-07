using ASystem.RandomMessageGeneration;

namespace ASystem;

public interface IApiClient
{
    public Task PostAsync(string path, PrioritizedMessage content);
}