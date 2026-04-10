using System;

namespace Shared.Dtos;

public class WorkloadDto
{
    public int Vmid { get; set; }
    public string? Type { get; set; } = "";
    public string? Name { get; set; } = "";
    public string? Status { get; set; } = "";
    public string? IpAddress { get; set; } = "";
    public int CpuCores { get; set; } = 0;
    public long MemoryMax { get; set; }
}
