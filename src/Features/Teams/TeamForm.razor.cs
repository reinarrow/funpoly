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
        private int? teamCookie;

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
                teamCookie = await localStorage.GetItemAsync<int?>("teamCookie");

                if (teamCookie != null)
                {
                    var userTeam = gameManager.GetGame().Teams.Find(team => team.Id == teamCookie);
                    if (userTeam == null)
                    {
                        // Cookie is from previous game. Remove it
                        await localStorage.RemoveItemAsync("teamCookie");
                        teamCookie = null;
                    }
                }

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
            // Set initialization flag to false to force cookie acquisition
            isInitialised = false;

            // Add the team to the game manager and notify clients
            await gameManager.AddTeam(team);

            // Acquire assigned Id to store cookie

            teamCookie = await gameManager.GetTeamId(team.Name);
            await localStorage.SetItemAsync("teamCookie", teamCookie);

            // Trigger update once the cookie has been set
            // await Update();
        }
    }
}