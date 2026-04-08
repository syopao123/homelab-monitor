using System;
using Shared.Dtos;

namespace Api.Services;

public interface IDashboardManagerService
{
    Task<NodeDto> GetNodeInformationAsync(string name);
}
