using Shared.Dtos;

namespace Web.Services;

public class HostService : IHostService
{
    private readonly IHomeLabApiService _api;

    public HostService(IHomeLabApiService api)
    {
        _api = api;
    }

    public async Task<string> CheckBackendStatusAsync()
    {
        return await _api.CheckActiveHostStatusAsync();
    }

    public async Task<List<ProxmoxHostDto>> GetHostsAsync()
    {
        return await _api.GetHostsAsync();
    }

    public async Task RegisterHostAsync(CreateHostDto hostDto)
    {
        await _api.RegisterHostAsync(hostDto);
    }

    public Task<bool> SetActiveHostAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
