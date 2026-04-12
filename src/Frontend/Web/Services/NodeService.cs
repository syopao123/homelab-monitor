using System;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Dtos.Nodes;

namespace Web.Services;

public class NodeService : INodeService
{
    private readonly IHomeLabApiService _apiService;
    private readonly HttpClient _httpClient;

    public NodeService(HttpClient client, IHomeLabApiService apiService)
    {
        _httpClient = client;
        _apiService = apiService;
    }

    public async Task<NodeDashboardDto> GetNodeDashboardAsync(string nodeName)
    {
        return await _apiService.GetNodeDashboardDtoAsync(nodeName);
    }

    public async Task<string> GetSelectedNodeNameAsync()
    {
        var nodeName = await _apiService.GetSelectedNodeNameAsync();
        return nodeName;
    }

    public async Task<List<NodeDto>> GetNodesAsync()
    {
        try
        {
            var nodes = await _httpClient.GetFromJsonAsync<List<NodeDto>>("api/Node/list");
            if (nodes is null)
                return new List<NodeDto>();
            return nodes;
        }
        catch (Exception)
        {
            return new List<NodeDto>();
        }
    }

    public async Task<bool> UpdateSelectedNodeAsync(string nodeName)
    {
        var result = await _httpClient.PatchAsJsonAsync($"api/Node/{nodeName}/activate", new { });
        if (!result.IsSuccessStatusCode)
            return false;
        return true;
    }
}
