using Funpoly.Data.Models;
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

        // Team corresponding to the user using the device. Obtained by the browser cookie.
        private Team userTeam;

        private Modal bankerModal;
        private decimal lastPaymentAmount;

        private Modal surpriseCardModalRef;
        private string surpriseCardText;

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
                    gameManager.OnSurpriseCard += async (teamId, text) => await OnSurpriseCardReceived(teamId, text);
                }
                else
                {
                    await GetTeamCookie();
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

        /// <summary>
        ///     Read team cookie and save the team to the cascading parameter userTeam
        /// </summary>
        /// <returns></returns>
        public async Task GetTeamCookie()
        {
            //Get cookie for team (needs to be done to work on redirection)
            int? teamCookie = await localStorage.GetItemAsync<int>("teamCookie");
            if (teamCookie != null)
            {
                userTeam = gameManager.GetGame().Teams.Find(team => team.Id == teamCookie);
                if (userTeam != null)
                {
                    // User belongs to a team from the current game. Subscribe to teams notifications
                    gameManager.OnSurpriseCard += async (teamId, text) => await OnSurpriseCardReceived(teamId, text);
                }
                else
                {
                    // Cookie is from previous game. Remove it
                    await localStorage.RemoveItemAsync("teamCookie");
                    teamCookie = null;
                }
            }
        }

        public void Dispose()
        {
            gameManager.OnChange -= Update; // TODO: Check this dispose
            if (isBanker)
            {
                gameManager.OnBankPayment -= ShowBankerModal;
                gameManager.OnSurpriseCard -= OnSurpriseCardReceived;
            }
            else if (userTeam != null)
            {
                gameManager.OnSurpriseCard -= OnSurpriseCardReceived;
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

        private async Task OnSurpriseCardReceived(int teamId, string text)
        {
            // Only show card to banker and corresponding team
            if (isBanker || userTeam.Id == teamId)
            {
                surpriseCardText = text;
                surpriseCardModalRef.Show();
            }
        }
    }
}