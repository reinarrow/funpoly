using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Properties
{
    public partial class ParcelList : ComponentBase
    {
        [Parameter]
        public Continent Continent { get; set; }

        private bool isInitialized = false;
        private List<Parcel> parcels;

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
            parcels = await gameManager.GetContinentParcelsWithProperties(Continent.Id);

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