using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funpoly.Features.Games.GameList
{
    public partial class GameList : ComponentBase
    {       
        private List<Game> games;

        protected async override Task OnInitializedAsync()
        {
            games = await repository.GetAllAsync(); // TODO: Get in progress only
            await base.OnInitializedAsync();
        }
    }
}
