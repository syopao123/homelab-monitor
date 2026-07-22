using System;
using Shared.Dtos;

namespace Web.Services;

public interface IHostService
{
    Task<string> CheckBackendStatusAsync();
    Task<List<ProxmoxHostDto>> GetHostsAsync();
    Task<bool> SetActiveHostAsync(Guid id);
    Task RegisterHostAsync(CreateHostDto hostDto);
}
