using System.Threading.Tasks;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Pages
{
    public partial class GameView
    {
        [CascadingParameter(Name = "IsBanker")]
        public bool IsBanker { get; set; }

        [CascadingParameter(Name = "UserTeam")]
        protected Team UserTeam { get; set; }

        private bool isInitialised = false;

        private string selectedTab = "money";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!isInitialised)
            {
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