using System;
using Api.Data;
using Api.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;

namespace Api.Services;

public class ActivityLogsManagerService : IActivityLogsManagerService
{
    private readonly IProxmoxService _proxmoxService;
    private readonly ApplicationDbContext _context;

    public ActivityLogsManagerService(IProxmoxService proxmoxService, ApplicationDbContext context)
    {
        _proxmoxService = proxmoxService;
        _context = context;
    }

    public async Task<List<ActivityLogDto>> GetLogsAsync(string nodeName)
    {
        var activeHost = await _context.ProxmoxHosts.FirstOrDefaultAsync(h => h.IsActive);

        if (activeHost is null)
            throw new InvalidHostOperationException("No current active host");
        
        if (activeHost.SelectedNodeName != nodeName)
            throw new InvalidNodeOperationException("Node does not match given node name");

        var data = await _proxmoxService.GetLogsAsync(nodeName, new ProxmoxHostDto
        {
            HostName = activeHost.HostName,
            ServerUrl = activeHost.ServerUrl,
            ApiToken = activeHost.ApiToken
        });

        var activityLogs = new List<ActivityLogDto>();

        foreach (var item in data.EnumerateArray())
        {
            activityLogs.Add(new ActivityLogDto
            {
                StartTime = DateTimeOffset.FromUnixTimeSeconds(item.GetProperty("starttime").GetInt64()).DateTime,
                EndTime = item.TryGetProperty("endtime", out var e) 
                ? DateTimeOffset.FromUnixTimeSeconds(e.GetInt64()).DateTime 
                : null,
                Action = item.GetProperty("type").GetString() ?? "Action type not found",
                User = item.GetProperty("user").GetString() ?? "User not found",
                Status = item.TryGetProperty("status", out var statusElement) ?  statusElement.GetString() : "In Progress",
            });
        }
        return activityLogs;
    }
}
