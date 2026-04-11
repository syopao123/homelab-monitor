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

    public async Task<JsonElement> GetNodesAsync(string url, string token)
    {
        try
        {
            _httpClient.BaseAddress = new Uri(url.TrimEnd('/') + "/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                "Authorization",
                $"PVEAPIToken={token}"
            );

            var root = await _httpClient.GetFromJsonAsync<JsonElement>("api2/json/nodes");
            var data = root.GetProperty("data");
            return data;
        }
        catch (Exception ex)
        {
            throw new InvalidHostOperationException(ex.Message);
        }
    }

    // Helper method
    private HttpRequestMessage CreateRequest(string path, ProxmoxHostDto host)
    {
        var url = $"{host.ServerUrl.TrimEnd('/')}/{path.TrimStart('/')}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.TryAddWithoutValidation("Authorization", $"PVEAPIToken={host.ApiToken}");
        return request;
    }

    public async Task<JsonElement> GetNodeStatusAsync(string nodeName, ProxmoxHostDto host)
    {
        using var request = CreateRequest($"api2/json/nodes/{nodeName}/status", host);
        var response = await _httpClient.SendAsync(request);
        try
        {
            // Return node status information
            var root = await response.Content.ReadFromJsonAsync<JsonElement>();
            return root.GetProperty("data");
        }
        catch (Exception ex)
        {
            throw new InvalidNodeOperationException($"Failed to get node information. {ex.Message}");
        }
    }

    public async Task<JsonElement> GetNodeVmsAsync(string nodeName, ProxmoxHostDto host)
    {
        using var request = CreateRequest($"api2/json/nodes/{nodeName}/qemu", host);
        var response = await _httpClient.SendAsync(request);

        try
        {
            // Return vms info
            var root = await response.Content.ReadFromJsonAsync<JsonElement>();
            return root.GetProperty("data");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<JsonElement> GetNodeLxcAsync(string nodeName, ProxmoxHostDto host)
    {
        using var request = CreateRequest($"api2/json/nodes/{nodeName}/lxc", host);
        var response = await _httpClient.SendAsync(request);
        try
        {
            // Return lxcs info
            var root = await response.Content.ReadFromJsonAsync<JsonElement>();
            return root.GetProperty("data");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<JsonElement> GetStorageListAsync(string nodeName, ProxmoxHostDto host)
    {
        using var request = CreateRequest($"api2/json/nodes/{nodeName}/storage", host);
        var response = await _httpClient.SendAsync(request);
        
        try
        {
            var root = await response.Content.ReadFromJsonAsync<JsonElement>();
            return root.GetProperty("data");            
        } catch (Exception)
        {
            throw;
        }
    }
}
