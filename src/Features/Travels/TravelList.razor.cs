using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Travels
{
    public partial class TravelList : ComponentBase
    {
        private bool isInitialized = false;
        private List<Team> teams;

        protected override async Task OnInitializedAsync()
        {
            //Declare callback for SignalR (need to update in case parcel properties change)
            gameManager.OnChange += async () => await Update();

            // Call update for the first time to obtain data
            await Update();

            await base.OnInitializedAsync();
            isInitialized = true;
        }

        private async Task Update()
        {
            // Obtain teams list with travel data from gamemanager
            teams = await gameManager.GetTeamsWithTravelData();

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
    }
}