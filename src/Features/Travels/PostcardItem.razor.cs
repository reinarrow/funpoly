using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Travels
{
    public partial class PostcardItem : ComponentBase
    {
        [Parameter]
        public Postcard Postcard { get; set; }

        [Parameter]
        public Team Team { get; set; }

        [CascadingParameter(Name = "IsBanker")]
        protected bool IsBanker { get; set; }

        private Modal modalRef;
        private Modal imageModalRef;

        private PostcardTeam postcardTeam;

        protected override async Task OnParametersSetAsync()
        {
            postcardTeam = Postcard.PostcardTeams.SingleOrDefault(pt => pt.TeamId == Team.Id);

            await base.OnParametersSetAsync();
        }

        private void OnPostcardClick()
        {
            // Only allow banker to modify
            if (IsBanker)
            {
                ShowModal();
            }
            else
            {
                ShowImageModal();
            }
        }

        private void ShowImageModal()
        {
            imageModalRef.Show();
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
            HideModal();
            if (postcardTeam != null)
            {
                await gameManager.RemovePostcardTeam(postcardTeam);
            }
            else
            {
                //Create and save parcel property
                var newPostcardTeam = new PostcardTeam
                {
                    TeamId = Team.Id,
                    PostcardId = Postcard.Id
                };

                await gameManager.CreatePostcardTeam(newPostcardTeam);
            }
        }
    }
}