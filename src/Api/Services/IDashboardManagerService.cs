using System;
using Shared.Dtos;

namespace Api.Services;

public interface IDashboardManagerService
{
    Task<NodeInfoDto> GetNodeInformationAsync(string name);
}
