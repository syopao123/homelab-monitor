using Shared.Dtos;

namespace Api.Services;

public class ProxmoxService : IProxmoxService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProxmoxService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> TestConnectionAsync(CreateHostRequestDto request)
    {
        var client = _httpClientFactory.CreateClient("TestProxmoxConnection");

        string baseUrl = request.ServerUrl;
        client.BaseAddress = new Uri(baseUrl);
        Console.WriteLine($"PVEAPIToken={request.ApiToken}");
        client.DefaultRequestHeaders.TryAddWithoutValidation(
            "Authorization",
            $"PVEAPIToken={request.ApiToken}"
        );
        try
        {
            // Check for response
            var response = await client.GetAsync("api2/json/version");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection failed: {ex.Message}");
            return false;
        }
    }
}
