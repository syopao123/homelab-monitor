using System;
using Api.Data;
using Api.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Api.Services;

public class DashboardManagerService : IDashboardManagerService
{
    private readonly ApplicationDbContext _context;
    private readonly IProxmoxService _proxmoxService;

    public DashboardManagerService(ApplicationDbContext context, IProxmoxService proxmoxService)
    {
        _context = context;
        _proxmoxService = proxmoxService;
    }

    public async Task<NodeInfoDto> GetNodeInformationAsync(string name)
    {
        // Get active host
        var activeHost = await _context.ProxmoxHosts.FirstOrDefaultAsync(h => h.IsActive == true);

        if (activeHost is null)
            throw new InvalidHostOperationException("No active host found.");

        string url = activeHost.ServerUrl;
        string token = activeHost.ApiToken;
        string nodeName = name;
        var nodeInfo = await _proxmoxService.GetNodeInfoAsync(url, token, nodeName);
        return nodeInfo;
    }
}
