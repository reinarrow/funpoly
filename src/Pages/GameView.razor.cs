using Microsoft.AspNetCore.Components;

namespace Funpoly.Pages
{
    public partial class GameView
    {
        [CascadingParameter]
        public bool IsBanker { get; set; }

        private string selectedTab = "money";

        private void OnSelectedTabChanged(string name)
        {
            selectedTab = name;
        }
    }
}