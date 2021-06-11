using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.BankerActions
{
    public partial class BankerActionListItem : ComponentBase
    {
        [Parameter]
        public string IconSource { get; set; }

        [Parameter]
        public string ModalTitle { get; set; }

        [Parameter]
        public EventCallback<int> ModalAction { get; set; }

        private Modal modalRef;
        private int selectedTeam;

        private void ShowModal()
        {
            modalRef.Show();
        }

        private void HideModal()
        {
            modalRef.Hide();
        }

        private async Task SaveChanges()
        {
            await ModalAction.InvokeAsync(selectedTeam);
            HideModal();
        }
    }
}