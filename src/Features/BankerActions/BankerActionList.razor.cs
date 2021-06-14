using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Funpoly.Data.Models;
using Blazorise;

namespace Funpoly.Features.BankerActions
{
    public partial class BankerActionList : ComponentBase
    {
        private Random rand;
        private Modal surpriseCardModalRef;
        private string surpriseCardText;

        private Modal registerLapModalRef;
        private int modalSelectedTeam;
        private int modalTravelDays;
        private int modalTransportId;

        private List<Transport> availableTransports;

        protected override async Task OnInitializedAsync()
        {
            rand = new Random();
            availableTransports = await transportRepository.GetAllAsync();

            await base.OnInitializedAsync();
        }

        private async Task FinePerSpeedLimit(int teamId)
        {
        }

        private async Task GetSurpriseCard(int teamId)
        {
            // Extract a random surprise card and show it on screen
            int surpriseCardsCount = surpriseCardRepository.GetCount();
            SurpriseCard card = await surpriseCardRepository.GetByIdAsync(rand.Next(1, surpriseCardsCount));

            surpriseCardText = card.Text;
            surpriseCardModalRef.Show();
        }

        private async Task GiveLotteryPrize(int teamId)
        {
            await gameManager.GiveLotteryPrizeToTeam(teamId);
        }

        private async Task ShowLapModal(int teamId)
        {
            modalSelectedTeam = teamId;
            modalTravelDays = 0;
            modalTransportId = availableTransports.First().Id;

            registerLapModalRef.Show();
        }

        private void HideLapModal()
        {
            registerLapModalRef.Hide();
        }

        private async Task RegisterLap()
        {
            HideLapModal();
            await gameManager.RegisterTeamLap(modalSelectedTeam, modalTravelDays, modalTransportId);
        }
    }
}