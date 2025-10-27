using Microsoft.AspNetCore.Components;
using spaceTracker.Models;
using spaceTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spaceTracker.Components.Pages.Programs
{
    public partial class Programs : ComponentBase
    {
        [Inject] private SpaceDevsService spaceDevsService { get; set; } = default!;

        private List<SpaceProgram> programs = new();
        private HashSet<int> loadedProgramIds = new();
        private bool isLoading = false;
        private string? errorMessage = null;
        private int currentOffset = 0;
        private const int BatchSize = 40;

        protected override async Task OnInitializedAsync()
        {
            await LoadNextBatchAsync();
        }

        private async Task LoadNextBatchAsync()
        {
            isLoading = true;
            errorMessage = null;

            try
            {
                var programBatch = await spaceDevsService.SpaceProg(limit: BatchSize, offset: currentOffset);

                if (programBatch.Any())
                {
                    foreach (var program in programBatch)
                    {
                        if (!loadedProgramIds.Contains(program.Id))
                        {
                            loadedProgramIds.Add(program.Id);
                            programs.Add(program);
                        }
                    }

                    programs = programs
                        .OrderBy(p => p.StartDate ?? DateTime.MaxValue)
                        .ToList();

                    currentOffset += BatchSize;
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to load programs: {ex.Message}";
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }
    }
}
