using System.Threading.Tasks;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Pages
{
    public partial class GameView
    {
        [CascadingParameter(Name = "IsBanker")]
        public bool IsBanker { get; set; }

        private bool isInitialised = false;

        // Team corresponding to the user using the device. Obtained by the browser cookie.
        private Team userTeam;

        private string selectedTab = "money";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!isInitialised)
            {
                //Get cookie for team (needs to be done to work on redirection)
                int? teamCookie = await localStorage.GetItemAsync<int>("teamCookie");
                if (teamCookie != null)
                {
                    userTeam = gameManager.GetGame().Teams.Find(team => team.Id == teamCookie);
                    if (userTeam == null)
                    {
                        // Cookie is from previous game. Remove it
                        await localStorage.RemoveItemAsync("teamCookie");
                        teamCookie = null;
                    }
                }

                isInitialised = true;
                await InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }
        }

        private void OnSelectedTabChanged(string name)
        {
            selectedTab = name;
        }
    }
}