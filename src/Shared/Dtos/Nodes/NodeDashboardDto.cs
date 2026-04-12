namespace Shared.Dtos.Nodes;

public class NodeDashboardDto : NodeDto
{
    public string? PveVersion { get; set; } = "";
    public string? KernelVersion { get; set; } = "";
    public long Uptime { get; set; }
    public string UptimeDisplay => ToReadableUptime(Uptime);


    public static string ToReadableUptime(long totalSeconds)
    {
        if (totalSeconds <= 0) return "Offline";

        TimeSpan t = TimeSpan.FromSeconds(totalSeconds);

        // Format: 2d 03h 15m
        if (t.TotalDays >= 1)
        {
            return string.Format("{0}d {1:D2}h {2:D2}m",
                (int)t.TotalDays,
                t.Hours,
                t.Minutes);
        }

        // Format: 03h 15m
        if (t.TotalHours >= 1)
        {
            return string.Format("{0:D2}h {1:D2}m",
                t.Hours,
                t.Minutes);
        }

        // Format: 15m 30s
        return string.Format("{0}m {1}s",
            t.Minutes,
            t.Seconds);
    }

    public NodeMetricsDto Metrics { get; set; } = new();
    public ResourceCountDto Vms { get; set; } = new();
    public ResourceCountDto Lxcs { get; set; } = new();
}