using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Money
{
    public partial class MoneyList : ComponentBase
    {
        [CascadingParameter(Name = "UserTeam")]
        protected Team UserTeam { get; set; }

        [CascadingParameter(Name = "IsBanker")]
        protected bool IsBanker { get; set; }

        private decimal modalCash;

        private Modal modalRef;

        protected override async Task OnInitializedAsync()
        {
            //Declare callback for SignalR
            gameManager.OnChange += async () => await Update();

            await base.OnInitializedAsync();
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

        private async Task OnBankButtonClick()
        {
            ShowModal();
        }

        private void ShowModal()
        {
            modalCash = 0;
            modalRef.Show();
        }

        private void HideModal()
        {
            modalRef.Hide();
        }

        private async Task SaveChanges()
        {
            // Remove transferred money from user's team
            var newUserCash = UserTeam.Cash - modalCash;
            await gameManager.UpdateTeamCash(UserTeam, newUserCash);

            HideModal();
        }
    }
}