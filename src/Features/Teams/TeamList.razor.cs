using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Teams
{
    public partial class TeamList : ComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            //Declare callback for SignalR
            gameManager.OnChange += async () => await Update();

            await base.OnInitializedAsync();
        }

        private async Task Update()
        {
            // Trigger re-render
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        public void Dispose()
        {
            gameManager.OnChange -= Update; // TODO: Check this dispose
        }

        private async Task OnButtonClick()
        {
            await gameManager.StartGame();
        }
    }
}