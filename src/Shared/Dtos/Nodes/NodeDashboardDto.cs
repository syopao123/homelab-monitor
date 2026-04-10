namespace Shared.Dtos.Nodes;

public class NodeDashboardDto : NodeDto
{
    public string? PveVersion { get; set; } = "";
    public string? KernelVersion { get; set; } = "";
    public long Uptime { get; set; }

    public NodeMetricsDto Metrics { get; set; } = new();
    public ResourceCountDto Vms { get; set; } = new();
    public ResourceCountDto Lxcs { get; set; } = new();
}