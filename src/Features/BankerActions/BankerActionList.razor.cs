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
        private string surpriseCardTeamName;

        protected override Task OnInitializedAsync()
        {
            rand = new Random();
            return base.OnInitializedAsync();
        }

        private async Task FinePerSpeedLimit(int teamId)
        {
        }

        private async Task GetSurpriseCard(int teamId)
        {
            // Extract a random surprise card and show it on screen
            int surpriseCardsCount = surpriseCardRepository.GetCount();
            SurpriseCard card = await surpriseCardRepository.GetByIdAsync(rand.Next(surpriseCardsCount));

            surpriseCardText = card.Text;
            surpriseCardModalRef.Show();
        }

        private async Task GiveLotteryPrize(int teamId)
        {
        }

        private async Task RegisterLap(int teamId)
        {
        }
    }
}