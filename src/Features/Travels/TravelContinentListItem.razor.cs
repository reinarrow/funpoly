using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Travels
{
    public partial class TravelContinentListItem : ComponentBase
    {
        [Parameter]
        public Continent Continent { get; set; }

        [Parameter]
        public Team Team { get; set; }

        private bool isInitialized = false;
        private List<Postcard> postcards;

        protected override async Task OnInitializedAsync()
        {
            //Declare callback for SignalR
            gameManager.OnChange += async () => await Update();

            await Update();
            await base.OnInitializedAsync();
            isInitialized = true;
        }

        private async Task Update()
        {
            // Obtain fixed continents list from DB
            postcards = await gameManager.GetPostcardsByContinent(Continent);

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