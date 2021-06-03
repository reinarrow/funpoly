using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Funpoly.Data.Models;
using Blazorise;

namespace Funpoly.Features.Travels
{
    public partial class TravelListItem : ComponentBase
    {
        [Parameter]
        public Team Team { get; set; }

        [CascadingParameter(Name = "IsBanker")]
        protected bool IsBanker { get; set; }

        [CascadingParameter(Name = "UserTeam")]
        protected Team UserTeam { get; set; }

        private bool collapseVisible;
        private bool isInitialized = false;

        // reference to the modal component
        private Modal modalRef;

        private int modalDays;
        private int? modalTransportId;

        private List<Transport> availableTransports;

        protected override async Task OnInitializedAsync()
        {
            availableTransports = await transportRepository.GetAllAsync();

            await base.OnInitializedAsync();

            isInitialized = true;
        }

        private void ShowModal()
        {
            modalDays = Team.Days;
            modalTransportId = Team.TransportId ?? 0;
            modalRef.Show();
        }

        private void HideModal()
        {
            modalRef.Hide();
        }

        private async Task SaveChanges()
        {
            //Update travel data (days and transport)
            if (modalTransportId == 0) modalTransportId = null;
            await gameManager.UpdateTeamTravelData(Team.Id, modalDays, modalTransportId);

            HideModal();
        }

        private string GetTransportIcon()
        {
            switch (Team.Transport.Name)
            {
                case "Avión":
                    return "image/plane.png";

                case "Globo":
                    return "image/balloon.png";

                case "Tren":
                    return "image/train.png";

                case "Barco":
                    return "image/boat.png";

                case "Elefante":
                    return "image/elephant.png";

                default:
                    return "";
            }
        }
    }
}