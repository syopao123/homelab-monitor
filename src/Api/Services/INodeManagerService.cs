using Shared.Dtos;
using Shared.Dtos.Nodes;

namespace Api.Services;

public interface INodeManagerService
{
    Task<string> GetActiveNodeNameAsync();
    Task<List<NodeDto>> GetNodesAsync();
    Task<bool> ActivateNodeAsync(string nodeName);
    Task<NodeDashboardDto> GetNodeDashboardAsync(string nodeName);
}
