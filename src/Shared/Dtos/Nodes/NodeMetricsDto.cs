using System;

namespace Shared.Dtos.Nodes;

public class NodeMetricsDto
{
    public double CpuUsage { get; set; }
    public long MemoryUsed { get; set; }
    public long MemoryTotal { get; set; }
    public long StorageUsed { get; set; }
    public long StorageTotal { get; set; }
}
