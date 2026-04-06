using System;
using Shared.Dtos;

namespace Web.Services;

public class HostService : IHostService
{
    private readonly HttpClient _http;

    public HostService(HttpClient client)
    {
        _http = client;
    }

    public async Task<List<ProxmoxHostDto>> GetHostsAsync()
    {
        return await _http.GetFromJsonAsync<List<ProxmoxHostDto>>("api/Host/proxmox-hosts")
            ?? new List<ProxmoxHostDto>();
    }

    public Task<bool> SetActiveHostAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
