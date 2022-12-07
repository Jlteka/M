using System.Net.Http.Json;
using System.Web.Http;
using ASystem.RandomMessageGeneration;
using Microsoft.Extensions.Logging;

namespace ASystem.BSystemClient;

public class BSystemApiClient : IApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BSystemApiClient> _logger;

    public BSystemApiClient(IHttpClientFactory factory, ILogger<BSystemApiClient> logger)
    {
        _logger = logger;
        _httpClient = factory.CreateClient("BSystem");
    }

    public async Task PostAsync(string path, PrioritizedMessage content)
    {
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }
        try
        {
            var response = await _httpClient.PostAsJsonAsync(path, content);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Message {content.MessageString} with priority {content.Priority} has been sent");
            }
            else
            {
                throw new HttpResponseException(response);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}