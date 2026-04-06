using System;

namespace Shared.Dtos;

public class NodeInfoDto
{
    public string? NodeName { get; set; }
    public string? PveVersion { get; set; }
    public string? KernelVersion { get; set; }
    public string? Uptime { get; set; }
}
