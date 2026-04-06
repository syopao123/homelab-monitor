using System;
using Shared.Dtos;

namespace Web.Services;

public interface IHostService
{
    Task<List<ProxmoxHostDto>> GetHostsAsync();
    Task<bool> SetActiveHostAsync(Guid id);
}
