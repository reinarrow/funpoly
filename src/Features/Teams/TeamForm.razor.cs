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

        [CascadingParameter(Name = "UserTeam")]
        protected Team UserTeam { get; set; }

        [Parameter]
        public EventCallback GetTeamCookie { get; set; }

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
            var teamCookie = await gameManager.GetTeamId(team.Name);
            await localStorage.SetItemAsync("teamCookie", teamCookie);

            // Call Index method GetTeamCookie to update cascading parameter UserTeam
            await GetTeamCookie.InvokeAsync();
        }
    }
}