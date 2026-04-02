using System;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Web.Services;

public class LayoutState
{
    private readonly ProtectedLocalStorage _storage;

    // 1. Store the theme state (e.g., true = Dark, false = Light)
    public bool IsDarkMode { get; private set; } = false;

    // 2. The notification bell
    public event Action? OnChange;

    public LayoutState(ProtectedLocalStorage storage)
    {
        _storage = storage;
    }

    public async Task LoadStateAsync()
    {
        var result = await _storage.GetAsync<bool>("isDarkMode");

        if (result.Success)
        {
            IsDarkMode = result.Value;
            NotifyStateChanged();
        }
    }

    // 3. The method to toggle the theme
    public async Task ToggleTheme()
    {
        IsDarkMode = !IsDarkMode;
        await _storage.SetAsync("isDarkMode", IsDarkMode);
        NotifyStateChanged();
    }

    // 4. Ring the bell
    private void NotifyStateChanged() => OnChange?.Invoke();
}
