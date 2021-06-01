using System.Threading.Tasks;
using Blazorise;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Money
{
    public partial class MoneyListItem : ComponentBase
    {
        [Parameter]
        public Team Team { get; set; }

        private decimal modalCash;

        [CascadingParameter(Name = "IsBanker")]
        protected bool IsBanker { get; set; }

        // reference to the modal component
        private Modal modalRef;

        private bool isInitialized = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            isInitialized = true;
        }

        private void ShowModal()
        {
            modalCash = Team.Cash;
            modalRef.Show();
        }

        private void HideModal()
        {
            modalRef.Hide();
        }

        private async Task SaveChanges()
        {
            await gameManager.UpdateTeamCash(Team, modalCash);
            HideModal();
        }
    }
}