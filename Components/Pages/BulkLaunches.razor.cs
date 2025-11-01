using Microsoft.AspNetCore.Components;
using spaceTracker.Models;
using spaceTracker.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace spaceTracker.Components.Pages
{
    public partial class BulkLaunches : ComponentBase
    {
        [Inject] private SpaceDevsService spaceDevsService { get; set; } = default!;

        private List<SpaceDevsLaunch> launches = new();
        private bool isLoading = true;
        private string? errorMessage;
        private const int BatchSize = 40;

        // Returns an inline style string for a two-stop gradient derived from a key (provider/name)
        public string GetGradientStyle(string? key)
        {
            if (string.IsNullOrEmpty(key)) key = "default";
            var sum = 0;
            foreach (var c in key) sum += c;
            var hue1 = sum % 360;
            var hue2 = (hue1 + 40) % 360;
            // Use a slightly different lightness for the second color
            return $"background: linear-gradient(135deg, hsl({hue1},65%,55%), hsl({hue2},65%,45%));";
        }

        public string GetInitials(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "?";
            var parts = name.Split(new[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
                return parts[0].Substring(0, 1).ToUpperInvariant();
            var first = parts[0].Substring(0, 1);
            var second = parts.Length > 1 ? parts[1].Substring(0, 1) : "";
            return (first + second).ToUpperInvariant();
        }

        // Map status strings/abbrev to CSS classes
        public string GetStatusClass(string? status)
        {
            if (string.IsNullOrWhiteSpace(status)) return "status-neutral";

            var key = status.Trim().ToLowerInvariant();

            // common mappings from TheSpaceDevs statuses
            if (key.Contains("go") || key.Contains("go for launch") || key.Contains("green") || key.Contains("success") || key.Contains("launch"))
                return "status-success";

            if (key.Contains("tbc") || key.Contains("to be confirmed") || key.Contains("tbd") || key.Contains("tentative") || key.Contains("planned"))
                return "status-warning";

            if (key.Contains("hold") || key.Contains("delayed") || key.Contains("scrub") || key.Contains("failure") || key.Contains("cancelled") || key.Contains("canceled"))
                return "status-danger";

            // fallback
            return "status-neutral";
        }

        public string GetStatusTooltip(string? status)
        {
            if (string.IsNullOrWhiteSpace(status)) return "Status unknown";
            var key = status.Trim().ToLowerInvariant();

            if (key.Contains("go") || key.Contains("go for launch") || key.Contains("green") || key.Contains("success") || key.Contains("launch"))
                return "Cleared: systems/go — launch expected to proceed.";

            if (key.Contains("tbc") || key.Contains("to be confirmed") || key.Contains("tbd") || key.Contains("tentative") || key.Contains("planned"))
                return "To Be Confirmed: details not final yet (time/window tentative).";

            if (key.Contains("hold") || key.Contains("delayed") || key.Contains("scrub") || key.Contains("failure") || key.Contains("cancelled") || key.Contains("canceled"))
                return "Delayed/Problem: the launch is on hold, delayed, scrubbed, or cancelled.";

            return "Status: " + status;
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadLaunchesAsync();
        }

        private async Task LoadLaunchesAsync()
        {
            try
            {
                isLoading = true;
                errorMessage = null;
                var result = await spaceDevsService.GetUpcomingLaunchesAsync(BatchSize, 0);
                if (result != null)
                {
                    launches = result;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }
    }
}
