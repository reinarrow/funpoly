using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.Components;

namespace Funpoly.Features.Properties
{
    public partial class ParcelListItem : ComponentBase
    {
        [Parameter]
        public Parcel Parcel { get; set; }

        [CascadingParameter(Name = "IsBanker")]
        protected bool IsBanker { get; set; }

        private ParcelProperty parcelProperty;

        // reference to the modal component
        private Modal modalRef;

        private int modalTeamId;
        private bool modalHotelBuilt = false;

        protected override async Task OnParametersSetAsync()
        {
            parcelProperty = Parcel.ParcelProperties.SingleOrDefault();

            await base.OnParametersSetAsync();
        }

        private string GetParcelColor()
        {
            if (parcelProperty == null)
            {
                return "darkgray";
            }
            else
            {
                return parcelProperty.Team.Color;
            }
        }

        private void ShowModal()
        {
            if (parcelProperty != null)
            {
                modalTeamId = parcelProperty.Team.Id;
                modalHotelBuilt = parcelProperty.HotelBuilt;
            }
            else
            {
                modalTeamId = 0;
                modalHotelBuilt = false;
            }
            modalRef.Show();
        }

        private void HideModal()
        {
            modalRef.Hide();
        }

        private async Task SaveChanges()
        {
            // Check if team or hotel built have changed
            if (parcelProperty == null)
            {
                if (modalTeamId != 0)
                {
                    //Create and save parcel property
                    var newParcelProperty = new ParcelProperty
                    {
                        ParcelId = Parcel.Id,
                        TeamId = modalTeamId,
                        HotelBuilt = modalHotelBuilt
                    };
                    await gameManager.CreateParcelProperty(newParcelProperty);
                }
            }
            else
            {
                if (modalTeamId == 0)
                {
                    // Remove parcel property
                    await gameManager.RemoveParcelProperty(parcelProperty);
                }
                else if (modalTeamId != parcelProperty.Team.Id || modalHotelBuilt != parcelProperty.HotelBuilt)
                {
                    // Edit parcel property
                    var editedParcelProperty = parcelProperty.ShallowCopy();
                    editedParcelProperty.TeamId = modalTeamId;
                    editedParcelProperty.HotelBuilt = modalHotelBuilt;

                    await gameManager.UpdateParcelProperty(editedParcelProperty);
                }
                // Otherwise, nothing changed. Don't do anything
            }

            HideModal();
        }
    }
}