using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Teams
{
    public partial class TeamForm : ComponentBase
    {
        private Team team;
        private bool isInitialised = false;
        private string teamCookie;

        protected override async Task OnInitializedAsync()
        {
            // Initialize team object
            team = new Team();

            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!isInitialised)
            {
                //Get cookie for team (needs to be done to work on redirection)
                teamCookie = await localStorage.GetItemAsync<string>("teamCookie");

                //TODO: Cookie needs to be removed when game finishes

                // Declare callback for SignalR
                gameManager.OnChange += async () => await Update();

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

        public void Dispose()
        {
            gameManager.OnChange -= Update; // TODO: Check this dispose
        }

        private async Task OnButtonClick()
        {
            // Fill cookie with team name
            await localStorage.SetItemAsync("teamCookie", team.Name);

            // Set initialization flag to false to force cookie acquisition
            isInitialised = false;

            // Add the team to the game manager and notify clients
            await gameManager.AddTeam(team);
        }
    }
}