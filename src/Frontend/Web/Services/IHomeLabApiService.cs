using System;
using Shared.Dtos;
using Shared.Dtos.Nodes;

namespace Web.Services;

public interface IHomeLabApiService
{
    Task<string> GetSelectedNodeNameAsync();
    Task<NodeDashboardDto> GetNodeDashboardDtoAsync(string nodeName);
    Task<List<WorkloadDto>> GetResourcesAsync(string nodeName);
    Task<List<ActivityLogDto>> GetActivityLogsAsync(string nodeName);
    
}
