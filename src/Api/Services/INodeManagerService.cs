using System;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Api.Services;

public interface INodeManagerService
{
    Task<List<NodeDto>> GetNodesAsync();
    Task<bool> UpdateSelectedNodeAsync(string nodeName);
}
