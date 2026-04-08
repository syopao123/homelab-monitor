using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Web.Services;

public interface INodeService
{
    Task<List<NodeDto>> GetNodesAsync();
    Task<bool> UpdateSelectedNodeAsync(string nodeName);
}
