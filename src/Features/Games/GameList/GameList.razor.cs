using Blazorise.DataGrid;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funpoly.Features.Games.GameList
{
    public partial class GameList : ComponentBase
    {
        private List<Game> games { get; set; }
        private Game selectedGame;

        protected async override Task OnInitializedAsync()
        {
            games = await gameRepository.GetAllAsync();
            await base.OnInitializedAsync();
        }

        private static void OnGameAdded(Game game)
        {
            game.Name = "Nuevo juego";
            game.Status = GameStatus.TeamsConfig;
            game.CreatedDate = DateTime.Now;
        }

        private async Task OnRowInserting(CancellableRowChange<Game, Dictionary<string, object>> arg)
        {
            var (result, _) = await gameRepository.AddAsync(arg.Item);
            if (result)
            {
                selectedGame = arg.Item;
            }
            else
            {
                arg.Cancel = true;
            }
        }

        private async Task OnRowUpdating(CancellableRowChange<Game, Dictionary<string, object>> arg)
        {
            //TODO: Update is not working in database
            var (result, _) = await gameRepository.UpdateAsync(arg.Item);
            if (!result)
            {
                arg.Cancel = true;
            }
        }

        private async Task OnRowRemoving(CancellableRowChange<Game> arg)
        {
            var (result, _) = await gameRepository.RemoveAsync(arg.Item);
            if (result)
            {
                if (selectedGame == arg.Item)
                {
                    selectedGame = null;
                }
            }
            else
            {
                arg.Cancel = true;
            }
        }

        private async Task OnStartButtonClick()
        {
            await gameManager.LoadGameById(selectedGame.Id);
        }
    }
}