using ASystem.RandomMessageGeneration;

namespace ASystem.BSystemClient;

public class BSystemClient : ISystemClient
{
    private readonly IApiClient _apiClient;

    public BSystemClient(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task SendMessageAsync(PrioritizedMessage message)
    {
        await _apiClient.PostAsync(BSystemApiRoutes.SendMessage, message);
    }
}