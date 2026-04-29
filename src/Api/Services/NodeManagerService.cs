using Api.Data;
using Api.Exceptions;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Dtos.Nodes;
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

    public async Task<string> GetSelectedNodeNameAsync()
    {
        var activeHost = await _context.ProxmoxHosts.FirstOrDefaultAsync<ProxmoxHost>(h => h.IsActive == true);
        if (activeHost is null)
            throw new InvalidHostOperationException("No active host found.");
        if (activeHost.SelectedNodeName is null)
            throw new InvalidHostOperationException("No selected node found.");
        return activeHost.SelectedNodeName;
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
            throw new InvalidHostOperationException("Node not found.");

        activeHost.SelectedNodeName = nodeName;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<NodeDashboardDto> GetNodeDashboardAsync(string nodeName)
    {
        var nodeDashboard = new NodeDashboardDto();

        // Check if given node matches the active host's selected node
        var activeHost = await _context.ProxmoxHosts.FirstOrDefaultAsync<ProxmoxHost>(h => h.IsActive == true);
        if (activeHost is null)
            throw new InvalidNodeOperationException("No active host found.");
        // Return null if mismatch
        // TODO: Throw a custom exception in the future
        if (activeHost.SelectedNodeName != nodeName)
            throw new InvalidNodeOperationException("Node does not match active host.");

        // Map host to DTO
        var host = new ProxmoxHostDto
        {
            HostName = activeHost.HostName,
            ServerUrl = activeHost.ServerUrl,
            ApiToken = activeHost.ApiToken,
        };

        // Retrieve metrics
        var metricsData = await _proxmoxService.GetNodeStatusAsync(nodeName, host);
        var metrics = new NodeMetricsDto
        {
            CpuUsage = metricsData.GetProperty("cpu").GetDouble(),
            MemoryUsed = metricsData.GetProperty("memory").GetProperty("used").GetInt64(),
            MemoryTotal = metricsData.GetProperty("memory").GetProperty("total").GetInt64(),
            StorageUsed = metricsData.GetProperty("rootfs").GetProperty("used").GetInt64(),
            StorageTotal = metricsData.GetProperty("rootfs").GetProperty("total").GetInt64()
        };

        // Retrieve lxc count
        var lxcData = await _proxmoxService.GetNodeLxcAsync(nodeName, host);
        var lxcs = new ResourceCountDto();
        foreach (var item in lxcData.EnumerateArray())
        {
            lxcs.Total++;
            if (item.GetProperty("status").GetString() == "running")
                lxcs.Running++;
        }

        // Retrieve vm count
        var vmData = await _proxmoxService.GetNodeVmsAsync(nodeName, host);
        var vms = new ResourceCountDto();
        foreach (var item in vmData.EnumerateArray())
        {
            vms.Total++;
            if (item.GetProperty("status").GetString() == "running")
                vms.Running++;
        }

        // Store data inside the dashboard before returning
        nodeDashboard.Status = "Online";
        nodeDashboard.NodeName = nodeName;
        nodeDashboard.PveVersion = metricsData.GetProperty("pveversion").GetString();
        nodeDashboard.KernelVersion = metricsData.GetProperty("current-kernel").GetProperty("release").GetString();
        nodeDashboard.Uptime = metricsData.GetProperty("uptime").GetInt64();
        nodeDashboard.IsSelected = activeHost.SelectedNodeName == nodeName;
        nodeDashboard.Metrics = metrics!;
        nodeDashboard.Lxcs = lxcs;
        nodeDashboard.Vms = vms;

        return nodeDashboard;
    }
}
