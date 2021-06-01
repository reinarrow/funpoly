using System.Threading.Tasks;
using Blazorise;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Teams
{
    public partial class TeamListItem : ComponentBase
    {
        [Parameter]
        public Team Team { get; set; }

        private Team modalTeam;

        [CascadingParameter(Name = "IsBanker")]
        protected bool IsBanker { get; set; }

        // reference to the modal component
        private Modal modalRef;

        private bool isInitialized = false;

        protected override async Task OnInitializedAsync()
        {
            modalTeam = Team.ShallowCopy();
            await base.OnInitializedAsync();

            isInitialized = true;
        }

        private void ShowModal()
        {
            modalTeam.Name = Team.Name;
            modalTeam.Color = Team.Color;
            modalRef.Show();
        }

        private void HideModal()
        {
            modalRef.Hide();
        }

        private async Task SaveChanges()
        {
            await gameManager.UpdateTeam(modalTeam);
            HideModal();
        }
    }
}