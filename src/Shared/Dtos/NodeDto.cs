using System;

namespace Shared.Dtos;

public class NodeDto
{
    public string? NodeName { get; set; }
    public string? PveVersion { get; set; }
    public string? KernelVersion { get; set; }
    public string? Uptime { get; set; }
    public string? Status { get; set; }
    public bool IsSelected { get; set; }
}
