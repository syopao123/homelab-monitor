using System;
using Shared.Dtos;

namespace Web.Services;

public interface IHostService
{
    Task<string> CheckApiStatusAsync();
    Task<List<ProxmoxHostDto>> GetHostsAsync();
    Task<bool> SetActiveHostAsync(Guid id);
}
