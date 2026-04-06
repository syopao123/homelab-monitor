using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class ProxmoxHost
{
    public Guid Id { get; set; }
    public required string HostName { get; set; }
    public required string ServerUrl { get; set; }
    public required string ApiToken { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = false;
}
