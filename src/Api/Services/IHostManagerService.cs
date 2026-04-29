using System;
using Api.Models;
using Shared.Dtos;

namespace Api.Services;

public interface IHostManagerService
{
    Task<ProxmoxHostDto> RegisterHostAsync(CreateHostRequestDto host);
    Task<IEnumerable<ProxmoxHost>> GetHostsAsync();
    Task<ProxmoxHostDto> GetHostByIdAsync(Guid id);
    Task<ProxmoxHost>? UpdateHostAsync(Guid id, UpdateHostDto dto);
    Task SetActiveHostAsync(Guid id);

    Task DeleteHostAsync(Guid id);
}
