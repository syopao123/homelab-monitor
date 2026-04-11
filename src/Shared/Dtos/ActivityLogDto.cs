using System;

namespace Shared.Dtos;

public class ActivityLogDto
{
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public required string Action { get; set; }
    public required string User { get; set; }
    public required string? Status { get; set; }
}
