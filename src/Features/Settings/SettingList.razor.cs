using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Settings
{
    public partial class SettingList : ComponentBase
    {
        private Modal modalRef;

        private async Task FinishGame()
        {
            HideModal();
            await gameManager.FinishGame();
        }

        private void ShowModal()
        {
            modalRef.Show();
        }

        private void HideModal()
        {
            modalRef.Hide();
        }
    }
}