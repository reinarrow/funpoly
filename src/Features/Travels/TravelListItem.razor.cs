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

        // reference to the modal component
        private Modal modalRef;
        private int modalDays;

        private void ShowModal()
        {
            modalDays = Team.Days;
            modalRef.Show();
        }

        private void HideModal()
        {
            modalRef.Hide();
        }

        private async Task SaveChanges()
        {
            // TODO: Update travel data (days, transport)

            HideModal();
        }

        private string GetTransportIcon()
        {
            if (Team.Transport.Name == "Avión")
            {
                return "image/plane.png";
            }
            else if (Team.Transport.Name == "Globo")
            {
                return "image/balloon.png";
            }
            else if (Team.Transport.Name == "Tren")
            {
                return "image/train.png";
            }
            else if (Team.Transport.Name == "Barco")
            {
                return "image/boat.png";
            }
            else if (Team.Transport.Name == "Elefante")
            {
                return "image/elephant.png";
            }
            else
            {
                return "";
            }
        }
    }
}

//new Transport { Name = "Avión", Dices = 2, Substraction = 0 },
//                    new Transport { Name = "Globo", Dices = 2, Substraction = 1 },
//                    new Transport { Name = "Tren", Dices = 1, Substraction = 0 },
//                    new Transport { Name = "Barco", Dices = 1, Substraction = 0 },
//                    new Transport { Name = "Elefante", Dices = 1, Substraction = 1 },