using System;
using Api.Data;
using Api.Exceptions;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Api.Services;

public class ResourcesManagerService : IResourcesManagerService
{
    private readonly ApplicationDbContext _context;
    private readonly IProxmoxService _proxmoxService;

    public ResourcesManagerService(ApplicationDbContext context, IProxmoxService proxmoxService)
    {
        _context = context;
        _proxmoxService = proxmoxService;
    }

    public async Task<List<WorkloadDto>> GetResourcesAsync(string nodeName)
    {
        var activeHost = await _context.ProxmoxHosts.FirstOrDefaultAsync(h => h.IsActive);
        if (activeHost is null)
            throw new InvalidHostOperationException("No active host found.");

        if (activeHost.SelectedNodeName != nodeName)
            throw new InvalidNodeOperationException("Node does not match active host.");

        var hostDto = new ProxmoxHostDto
        {
            HostName = activeHost.HostName,
            ServerUrl = activeHost.ServerUrl,
            ApiToken = activeHost.ApiToken,
        };

        var resources = new List<WorkloadDto>();
        // Get vms and lxcs json data
        var vmsTask = _proxmoxService.GetNodeVmsAsync(nodeName, hostDto);
        var lxcsTask = _proxmoxService.GetNodeLxcAsync(nodeName, hostDto);
        await Task.WhenAll(vmsTask, lxcsTask);
        var vmsData = vmsTask.Result;
        var lxcsData = lxcsTask.Result;
        // Start storing vms (Type should always be VM!)
        foreach (var item in vmsData.EnumerateArray())
        {
            resources.Add(new WorkloadDto
            {
                Vmid = item.GetProperty("vmid").GetInt32(),
                Type = "VM",
                Name = item.GetProperty("name").GetString(),
                Status = item.GetProperty("status").GetString(),
                IpAddress = "Temporary",
                CpuCores = item.GetProperty("cpus").GetInt32(),
                MemoryMax = item.GetProperty("maxmem").GetInt64(),
            });
        }
        // Start storing lxcs (Type should always be LXC!)
        foreach (var item in lxcsData.EnumerateArray())
        {
            resources.Add(new WorkloadDto
            {
                Vmid = item.GetProperty("vmid").GetInt32(),
                Type = "LXC",
                Name = item.GetProperty("name").GetString(),
                Status = item.GetProperty("status").GetString(),
                IpAddress = "Temporary",
                CpuCores = item.GetProperty("cpus").GetInt32(),
                MemoryMax = item.GetProperty("maxmem").GetInt64(),
            });
        }
        return resources.OrderBy(r => r.Vmid).ToList();
    }
}
