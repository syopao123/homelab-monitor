using System;
using Shared.Dtos;

namespace Web.Services;

public class ResourcesService : IResourcesService
{
    private readonly IHomeLabApiService _apiService;

    public ResourcesService(IHomeLabApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<List<WorkloadDto>> GetResourcesAsync(string nodeName)
    {
        return await _apiService.GetResourcesAsync(nodeName);
    }
}
