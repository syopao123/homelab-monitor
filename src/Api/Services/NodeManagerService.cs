using Api.Data;
using Api.Exceptions;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System.Linq;

namespace Api.Services;

public class NodeManagerService : INodeManagerService
{
    private readonly IProxmoxService _proxmoxService;
    private readonly ApplicationDbContext _context;


    public NodeManagerService(IProxmoxService proxmoxService, ApplicationDbContext context)
    {
        _proxmoxService = proxmoxService;
        _context = context;
    }

    public async Task<List<NodeDto>> GetNodesAsync()
    {
        var activeHost = await _context.ProxmoxHosts.FirstOrDefaultAsync<ProxmoxHost>(h => h.IsActive == true);
        if (activeHost is null)
            throw new InvalidHostOperationException("No active host found.");
        
        var data = await _proxmoxService.GetNodesAsync(activeHost.ServerUrl, activeHost.ApiToken);
        var nodes = new List<NodeDto>();
        foreach (var item in data.EnumerateArray())
        {
            var nodeName = item.GetProperty("node").GetString();
            nodes.Add(new NodeDto
            {
                NodeName = nodeName,
                Status = item.GetProperty("status").GetString(),
                IsSelected = activeHost.SelectedNodeName == nodeName,
            });
        }
        return nodes;
    }

    public async Task<bool> UpdateSelectedNodeAsync(string nodeName)
    {
        // Get active host
        var activeHost = await _context.ProxmoxHosts.FirstOrDefaultAsync<ProxmoxHost>(h => h.IsActive == true);
        if (activeHost is null)
            throw new InvalidHostOperationException("No active host found.");
        
        // Check if node name exists in active host
        var nodes = await GetNodesAsync();
        var nodeExists = nodes.Any(n => n.NodeName == nodeName);
        if (nodeExists is false)
            return false;

        activeHost.SelectedNodeName = nodeName;
        await _context.SaveChangesAsync();
        return true;
    }
}
