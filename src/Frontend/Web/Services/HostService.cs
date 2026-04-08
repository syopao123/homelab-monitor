using Shared.Dtos;

namespace Web.Services;

public class HostService : IHostService
{
    private readonly HttpClient _httpClient;

    public HostService(HttpClient client)
    {
        _httpClient = client;
    }

    public async Task<string> CheckApiStatusAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("ping");
            return response.IsSuccessStatusCode.ToString();
        }
        catch (HttpRequestException)
        {
            return $"API Connection Error";
        }
    }

    public async Task<List<ProxmoxHostDto>> GetHostsAsync()
    {
        try
        {
            // Get list of hosts
            var proxmoxHosts = await _httpClient.GetFromJsonAsync<List<ProxmoxHostDto>>("api/Host/proxmox-hosts");
            if (proxmoxHosts is null)
                return new List<ProxmoxHostDto>();
            return proxmoxHosts;
        }
        catch (Exception)
        {
            return new List<ProxmoxHostDto>();
        }
    }

    public Task<bool> SetActiveHostAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
