using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Funpoly.Features.Games.GameList
{
    public partial class GameListItem : ComponentBase
    {
        [Parameter] public Game Game { get; set; }


        #region UIEvents
        private async Task OnContinueButtonClick(int id)
        {
            await gameManager.LoadGameById(id);
        }
        #endregion
    }
}
