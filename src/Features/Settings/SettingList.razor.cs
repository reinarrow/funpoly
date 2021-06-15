using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using SettingsModel = Funpoly.Data.Models.Settings;

namespace Funpoly.Features.Settings
{
    public partial class SettingList : ComponentBase
    {
        private Modal modalRef;

        private bool isInitialized = false;

        // binded settings
        private SettingsModel bindedSettings;

        protected override async Task OnInitializedAsync()
        {
            //Declare callback for SignalR (need to update in case parcel properties change)
            gameManager.OnChange += async () => await Update();

            // Call update for the first time to obtain data
            await Update();

            await base.OnInitializedAsync();
            isInitialized = true;
        }

        private async Task Update()
        {
            // Get settings and bind them by shallow copy (to avoid accidentaly changing the original values)
            bindedSettings = gameManager.GetSettings().ShallowCopy();
        }

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

        private async Task SaveChanges()
        {
            await gameManager.SetSettingsAsync(bindedSettings);
        }
    }
}