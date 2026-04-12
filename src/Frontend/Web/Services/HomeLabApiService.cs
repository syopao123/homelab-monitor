using System;
using Shared.Dtos;
using Shared.Dtos.Nodes;

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

    public async Task<NodeDashboardDto> GetNodeDashboardDtoAsync(string nodeName)
    {
        try
        {
            var nodeDashboard = await _httpClient.GetFromJsonAsync<NodeDashboardDto>($"api/Node/{nodeName}/dashboard");
            if (nodeDashboard is null)
                throw new Exception("Node not found");
            return nodeDashboard;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<WorkloadDto>> GetResourcesAsync(string nodeName)
    {
        try
        {
            var resources = await _httpClient.GetFromJsonAsync<List<WorkloadDto>>($"api/Resources/{nodeName}");
            if (resources is null)
                throw new Exception("Node not found");
            return resources;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
