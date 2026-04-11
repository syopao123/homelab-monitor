using System;
using System.Runtime.CompilerServices;

namespace Shared.Dtos.Storage;

public class StorageDto
{
    public required string? Storage { get; set; } = "";
    public required string? Type { get; set; } = "";
    public string[]? ContentTypes { get; set; } = null;
    public required long Total { get; set; }
    public required long Used { get; set; }
    public required long Available { get; set; }
    public required bool IsActive { get; set; }
}
