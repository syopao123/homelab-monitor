using System;

namespace Web.Services;

public interface IHealthMonitorService
{
    bool IsOnline { get; }
    event Action? OnStatusChanged;
}
