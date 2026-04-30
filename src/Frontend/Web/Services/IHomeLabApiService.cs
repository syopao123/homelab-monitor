using System;
using Shared.Dtos;
using Shared.Dtos.Nodes;
using Shared.Dtos.Storage;

namespace Web.Services;

public interface IHomeLabApiService
{
    Task<string> GetSelectedNodeNameAsync();
    Task<NodeDashboardDto> GetNodeDashboardDtoAsync();
    Task<List<WorkloadDto>> GetResourcesAsync();
    Task<List<ActivityLogDto>> GetActivityLogsAsync();
    Task<List<StorageDto>> GetStorageAsync();
    
}
