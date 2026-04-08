namespace Web.Services;

public class HealthMonitorService : IHealthMonitorService, IDisposable
{
    private readonly HttpClient _httpClient;
    private PeriodicTimer? _timer;
    public bool IsOnline { get; private set; } = false;

    public event Action? OnStatusChanged;

    public HealthMonitorService(HttpClient client)
    {
        _httpClient = client;
        StartHeartbeat();
    }

    private async void StartHeartbeat()
    {
        // Initial check
        await CheckConnectionAsync();
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(10)); // Check every 10s
        while (await _timer.WaitForNextTickAsync())
        {
            await CheckConnectionAsync();
        }
    }

    private async Task CheckConnectionAsync()
    {
        var previousStatus = IsOnline;
        try
        {
            var response = await _httpClient.GetAsync("ping");
            IsOnline = response.IsSuccessStatusCode;
        }
        catch
        {
            IsOnline = false;
        }

        if (IsOnline != previousStatus)
        {
            OnStatusChanged?.Invoke(); // Trigger the "Shout"
        }
    }

    public void Dispose() => _timer?.Dispose();
}
