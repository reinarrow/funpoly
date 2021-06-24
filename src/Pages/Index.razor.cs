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

        private Modal notificationModalRef;
        private string notificationModalTitle;
        private string notificationModalText;

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
                    // Subscribe to notifications about payments and surprise cards
                    gameManager.OnBankPayment += async (amount) => await ShowBankerModal(amount);
                    gameManager.OnSurpriseCard += async (teamId, text) => await OnSurpriseCardReceived(teamId, text);
                    gameManager.OnTeamPayment += async (payingTeam, receivingTeam, amount) => await OnTeamPaymentReceived(payingTeam, receivingTeam, amount);
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
                userTeam = gameManager.GetGame()?.Teams?.Find(team => team.Id == teamCookie);
                if (userTeam != null)
                {
                    // User belongs to a team from the current game. Subscribe to teams notifications
                    gameManager.OnSurpriseCard += async (teamId, text) => await OnSurpriseCardReceived(teamId, text);
                    gameManager.OnTeamPayment += async (payingTeam, receivingTeam, amount) => await OnTeamPaymentReceived(payingTeam, receivingTeam, amount);
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
                notificationModalTitle = "Carta sorpresa";
                notificationModalText = text;
                notificationModalRef.Show();
            }
        }

        private async Task OnTeamPaymentReceived(Team payingTeam, Team receivingTeam, decimal amount)
        {
            // Cases when the notification should appear:
            // 1. The app user is the banker and he was not the one doing the transfer (payingTeam != null)
            // 2. The app user is not the banker and he is the receiving team
            if ((isBanker && payingTeam != null) || (!isBanker && userTeam != null && userTeam.Id == receivingTeam.Id))
            {
                notificationModalTitle = "Confirmación de Zumbi";
                notificationModalText = (payingTeam != null ? payingTeam.Name : "Banca")
                    + " -> "
                    + receivingTeam.Name
                    + ". Cantidad: "
                    + amount.ToString()
                    + " €";

                notificationModalRef.Show();
            }
        }
    }
}