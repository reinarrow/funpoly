using Funpoly.Data.Models;
using Funpoly.Data.Repositories.Interfaces;
using Funpoly.Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Funpoly.Pages
{
    public partial class Index : ComponentBase
    {
        [Parameter]
        public string bankerToken { get; set; }

        private bool isInitialised = false;
        private bool isBanker = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!isInitialised)
            {
                //Declare callback for SignalR
                gameManager.OnChange += async () => await Update();

                // Check token
                var envToken = Environment.GetEnvironmentVariable("BANKER_TOKEN");

                if (bankerToken != null && bankerToken == envToken)
                {
                    await localStorage.SetItemAsync("bankerCookie", "true");
                }

                //Get cookie (needs to be done to work on redirection)
                var cookieContent = await localStorage.GetItemAsync<string>("bankerCookie");

                if (cookieContent != null && cookieContent == "true")
                {
                    isBanker = true;
                    navManager.NavigateTo("/");
                }
                isInitialised = true;
                await Update();
            }
        }

        private async Task Update()
        {
            // Trigger re-render
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        private async Task SetGameStatus(GameStatus status)
        {
            var game = await gameRepository.GetAsync();
            game.Status = status;

            await gameRepository.UpdateAsync(game);
        }

        #region UIEvents

        private async Task OnStartButtonClick()
        {
            //Update game status
            await SetGameStatus(GameStatus.TeamsConfig);
            await gameManager.NotifyClientsAsync();
        }

        #endregion UIEvents

        public void Dispose()
        {
            gameManager.OnChange -= Update; // TODO: Check this dispose
        }
    }
}