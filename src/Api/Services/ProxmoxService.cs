using System.Text.Json;
using Api.Exceptions;
using Shared.Dtos;

namespace Api.Services;

public class ProxmoxService : IProxmoxService
{
    private readonly HttpClient _httpClient;

    public ProxmoxService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> TestConnectionAsync(CreateHostRequestDto request)
    {
        _httpClient.BaseAddress = new Uri(request.ServerUrl.TrimEnd('/') + "/");
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
            "Authorization",
            $"PVEAPIToken={request.ApiToken}"
        );
        try
        {
            // Check for response
            var response = await _httpClient.GetAsync("api2/json/version");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection failed: {ex.Message}");
            return false;
        }
    }

    public async Task<NodeInfoDto> GetNodeInfoAsync(string url, string token, string nodeName)
    {
        _httpClient.BaseAddress = new Uri(url.TrimEnd('/') + "/");
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
            "Authorization",
            $"PVEAPIToken={token}"
        );
        try
        {
            var root = await _httpClient.GetFromJsonAsync<JsonElement>(
                "api2/json/nodes/" + nodeName + "/status"
            );
            var data = root.GetProperty("data");
            var nodeInfo = new NodeInfoDto
            {
                NodeName = nodeName,
                PveVersion = data.GetProperty("pveversion").GetString(),
                KernelVersion = data.GetProperty("kversion").GetString(),
                Uptime = FormatUptime(data.GetProperty("uptime").GetInt64()),
            };
            return nodeInfo;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw new InvalidNodeOperationException("Failed to get node information.");
        }
    }

    private string FormatUptime(long seconds)
    {
        var t = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D} days, {1:D} hours", t.Days, t.Hours);
    }
}
