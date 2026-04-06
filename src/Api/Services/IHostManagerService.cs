using System;
using Api.Models;
using Shared.Dtos;

namespace Api.Services;

public interface IHostManagerService
{
    Task<ProxmoxHost> RegisterHostAsync(CreateHostRequestDto host);
    Task<IEnumerable<ProxmoxHost>> GetHostsAsync();
    Task<ProxmoxHost>? UpdateHostAsync(Guid id, UpdateHostDto dto);
    Task<bool> SetActiveHostAsync(Guid id);

    Task<bool> DeleteHostAsync(Guid id);
}
