using Microsoft.JSInterop;

namespace spaceTracker.Services;

public class CesiumService : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public CesiumService(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./cesium-interop.js").AsTask());
    }

    public async Task InitializeGlobeAsync(string containerId)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("initializeGlobe", containerId);
    }

    public async Task UpdateMarkerAsync(string entityId, double lat, double lon)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("updateMarker", entityId, lat, lon);
    }

    public async Task AddMarkerAsync(double lat, double lon, string name, string description)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("addMarker", lat, lon, name, description);
    }

    public async Task<string> AddISSMarkerAsync(double lat, double lon, string name, string description)
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<string>("addISSMarker", lat, lon, name, description);
    }

    public async Task FlyToLocationAsync(double lat, double lon)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("flyToLocation", lat, lon);
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            try
            {
                var module = await _moduleTask.Value;
                await module.InvokeVoidAsync("dispose");
                await module.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
                // Circuit disconnected, JS cleanup not possible
            }
        }
    }
}
