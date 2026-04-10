using Shared.Dtos;
using Shared.Dtos.Nodes;

namespace Api.Services;

public interface INodeManagerService
{
    Task<List<NodeDto>> GetNodesAsync();
    Task<bool> UpdateSelectedNodeAsync(string nodeName);
    Task<NodeDashboardDto> GetNodeDashboardAsync(string nodeName);
}
