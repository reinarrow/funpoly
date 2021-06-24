using System;
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

        [CascadingParameter(Name = "IsBanker")]
        protected bool IsBanker { get; set; }

        [CascadingParameter(Name = "UserTeam")]
        protected Team UserTeam { get; set; }

        // reference to the modal component
        private Modal bankerModalRef;

        private decimal bankerModalCash;

        private Modal playerModalRef;
        private decimal playerModalCash;

        private bool isInitialized = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            isInitialized = true;
        }

        private void ShowBankerModal()
        {
            bankerModalCash = Team.Cash;
            bankerModalRef.Show();
        }

        private void ShowPlayerModal()
        {
            if (IsBanker || (UserTeam != null && UserTeam.Id != Team.Id))
            {
                playerModalCash = 0;
                playerModalRef.Show();
            }
        }

        private void HideModal(Modal modalRef)
        {
            modalRef.Hide();
        }

        private async Task SendTransfer()
        {
            await gameManager.PayToTeam(UserTeam, Team, playerModalCash);

            HideModal(playerModalRef);
        }

        private async Task ModifyTeamCash()
        {
            // If the banker actually made any change
            if (Team.Cash != bankerModalCash)
            {
                await gameManager.UpdateTeamCash(Team, bankerModalCash);
            }
            HideModal(bankerModalRef);
        }

        private void ValidatePayment(ValidatorEventArgs args)
        {
            if (Convert.ToDecimal(args.Value) > 0)
            {
                args.Status = ValidationStatus.Success;
            }
            else
            {
                args.Status = ValidationStatus.Error;
                args.ErrorText = "La cantidad a transferir debe ser mayor que 0.";
            }
        }
    }
}