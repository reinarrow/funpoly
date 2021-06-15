using Funpoly.Data.Models;
using Funpoly.Data.Repositories.Interfaces;
using Funpoly.Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Blazorise;

namespace Funpoly.Pages
{
    public partial class Index : ComponentBase
    {
        [Parameter]
        public string bankerToken { get; set; }

        private bool isInitialised = false;
        private bool isBanker = false;

        private Modal bankerModal;
        private decimal lastPaymentAmount;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!isInitialised)
            {
                //Declare callback for SignalR
                gameManager.OnChange += async () => await Update();

                // Check token
                var envToken = Environment.GetEnvironmentVariable("BANKER_TOKEN");

                if (bankerToken != null && bankerToken == envToken)
                {
                    await localStorage.SetItemAsync("bankerCookie", "true");
                }

                //Get cookie (needs to be done to work on redirection)
                var cookieContent = await localStorage.GetItemAsync<string>("bankerCookie");

                if (cookieContent != null && cookieContent == "true")
                {
                    isBanker = true;
                    navManager.NavigateTo("/");
                }

                if (isBanker)
                {
                    // Subscribe to notifications about payments to bank
                    gameManager.OnBankPayment += async (amount) => await ShowBankerModal(amount);
                }

                isInitialised = true;
                await Update();
            }
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
            if (isBanker)
            {
                gameManager.OnBankPayment -= ShowBankerModal;
            }
        }

        private async Task ShowBankerModal(decimal amount)
        {
            lastPaymentAmount = amount;
            bankerModal.Show();
        }

        private async Task HideBankerModal()
        {
            bankerModal.Hide();
        }

        private async Task SendMoneyToLottery()
        {
            bankerModal.Hide();
            await gameManager.AddToLotteryPrize(lastPaymentAmount);
        }
    }
}