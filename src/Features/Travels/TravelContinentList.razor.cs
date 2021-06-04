using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Travels
{
    public partial class TravelContinentList : ComponentBase
    {
        [Parameter]
        public Team Team { get; set; }

        private bool isInitialized = false;
        private List<Continent> continents;

        protected override async Task OnInitializedAsync()
        {
            // Obtain fixed continents list from DB
            continents = await continentRepository.GetAllAsync();

            await base.OnInitializedAsync();
            isInitialized = true;

            await InvokeAsync(() => StateHasChanged());
        }
    }
}