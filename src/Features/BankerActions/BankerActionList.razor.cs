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

        private Modal speedLimitModalRef;
        private Team speedModalTeam = new Team();
        private bool speedModalFine = false;
        private bool speedModalReprimand = false;

        private List<Transport> availableTransports = new List<Transport>();

        protected override async Task OnInitializedAsync()
        {
            rand = new Random();
            await base.OnInitializedAsync();
        }

        private async Task ShowSpeedModal(int teamId)
        {
            speedModalTeam = gameManager.GetGame().Teams.SingleOrDefault(t => t.Id == teamId);
            speedModalReprimand = false;
            speedModalFine = false;

            speedLimitModalRef.Show();
        }

        private void HideSpeedModal()
        {
            speedLimitModalRef.Hide();
        }

        private async Task FinePerSpeedLimit()
        {
            HideSpeedModal();

            gameManager.FinePerSpeedLimit(speedModalTeam.Id, speedModalFine, speedModalReprimand);
        }

        private async Task GetSurpriseCard(int teamId)
        {
            // Extract a random surprise card and show it on screen
            int surpriseCardsCount = surpriseCardRepository.GetCount();
            SurpriseCard card = await surpriseCardRepository.GetByIdAsync(rand.Next(1, surpriseCardsCount+1));

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

            var allTransports = await transportRepository.GetAllAsync();
            availableTransports = allTransports.FindAll(transport => !gameManager.GetGame().Teams.Any(team => team.TransportId == transport.Id));
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