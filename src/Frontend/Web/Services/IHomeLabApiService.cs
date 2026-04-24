using System;
using Shared.Dtos;
using Shared.Dtos.Nodes;
using Shared.Dtos.Storage;

namespace Web.Services;

public interface IHomeLabApiService
{
    Task<string> GetSelectedNodeNameAsync();
    Task<NodeDashboardDto> GetNodeDashboardDtoAsync(string nodeName);
    Task<List<WorkloadDto>> GetResourcesAsync(string nodeName);
    Task<List<ActivityLogDto>> GetActivityLogsAsync(string nodeName);
    Task<List<StorageDto>> GetStoragesAsync(string nodeName);
    
}
