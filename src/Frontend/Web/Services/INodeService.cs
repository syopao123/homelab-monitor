using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Dtos.Nodes;

namespace Web.Services;

public interface INodeService
{
    Task<List<NodeDto>> GetNodesAsync();
    Task<bool> UpdateSelectedNodeAsync(string nodeName);

    Task<NodeDashboardDto> GetNodeDashboardAsync(string nodeName);
    Task<string> GetSelectedNodeNameAsync();
}
