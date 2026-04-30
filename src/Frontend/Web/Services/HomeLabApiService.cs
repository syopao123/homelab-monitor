using System;
using Shared.Dtos;
using Shared.Dtos.Nodes;
using Shared.Dtos.Storage;

namespace Web.Services;

public class HomeLabApiService : IHomeLabApiService
{
    private readonly HttpClient _httpClient;

    public HomeLabApiService(HttpClient client)
    {
        _httpClient = client;
    }

    public async Task<string> GetSelectedNodeNameAsync()
    {
        try
        {
            var nodeName = await _httpClient.GetStringAsync("api/Node/active-node");
            if (nodeName is null)
                throw new Exception("No active node found");
            return nodeName;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<NodeDashboardDto> GetNodeDashboardDtoAsync()
    {
        var nodeDashboard = await _httpClient.GetFromJsonAsync<NodeDashboardDto>($"api/Node/dashboard");
        if (nodeDashboard is null)
            throw new Exception("Node not found");
        return nodeDashboard;
    }

    public async Task<List<WorkloadDto>> GetResourcesAsync()
    {
        try
        {
            var resources = await _httpClient.GetFromJsonAsync<List<WorkloadDto>>($"api/Resources");
            if (resources is null)
                throw new Exception("Node not found");
            return resources;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<ActivityLogDto>> GetActivityLogsAsync()
    {
        var logs = await _httpClient.GetFromJsonAsync<List<ActivityLogDto>>($"api/ActivityLogs");
        if (logs is null)
            throw new Exception("Node not found");
        return logs;
    }

    public async Task<List<StorageDto>> GetStorageAsync()
    {
        var storages = await _httpClient.GetFromJsonAsync<List<StorageDto>>($"api/Storage");
        if (storages is null)
            throw new Exception("Node not found");
        return storages;
    }
}
