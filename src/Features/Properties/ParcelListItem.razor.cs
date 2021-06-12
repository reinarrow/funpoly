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

        [Parameter]
        public int GameId { get; set; }

        [CascadingParameter(Name = "IsBanker")]
        protected bool IsBanker { get; set; }

        private ParcelProperty parcelProperty;
        private decimal currentParcelTax;

        // reference to the modal component
        private Modal modalRef;

        private Modal displayModalRef;

        private int modalTeamId;
        private bool modalHotelBuilt = false;
        private bool modalWifi = false;
        private bool modalBuffet = false;
        private bool modalParking = false;
        private bool modalPool = false;

        protected override async Task OnParametersSetAsync()
        {
            // Get property in the current active game
            parcelProperty = Parcel.ParcelProperties.SingleOrDefault(prop => prop.Team.GameId == GameId);

            // Calculate current parcelTax
            await CalculateParcelTax();

            await base.OnParametersSetAsync();
        }

        private async Task CalculateParcelTax()
        {
            if (parcelProperty == null)
            {
                currentParcelTax = Parcel.Price / 2;
            }
            else
            {
                if (parcelProperty.HotelBuilt)
                {
                    currentParcelTax = Parcel.Price;
                }
                else
                {
                    currentParcelTax = Parcel.Price / 2;
                }

                // Each available service increments the tax by 50 €
                decimal servicesIncrement = 50 * (
                    (parcelProperty.WifiServiceAvailable ? 1 : 0)
                    + (parcelProperty.BuffetServiceAvailable ? 1 : 0)
                    + (parcelProperty.ParkingServiceAvailable ? 1 : 0)
                    + (parcelProperty.PoolServiceAvailable ? 1 : 0));

                currentParcelTax += servicesIncrement;

                // If the team owns all 4 parcels in the continent, the tax is doubled
                List<ParcelProperty> teamPropertiesInContinent = await parcelPropertyRepository.GetAllAsync(p => p.Where(p => p.TeamId == parcelProperty.TeamId && p.Parcel.ContinentId == Parcel.ContinentId));
                if (teamPropertiesInContinent.Count == 4)
                {
                    currentParcelTax *= 2;
                }
            }
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

        private void ShowDisplayModal()
        {
            displayModalRef.Show();
        }

        private void ShowModal()
        {
            if (parcelProperty != null)
            {
                modalTeamId = parcelProperty.Team.Id;
                modalHotelBuilt = parcelProperty.HotelBuilt;
                modalWifi = parcelProperty.WifiServiceAvailable;
                modalBuffet = parcelProperty.BuffetServiceAvailable;
                modalParking = parcelProperty.ParkingServiceAvailable;
                modalPool = parcelProperty.PoolServiceAvailable;
            }
            else
            {
                modalTeamId = 0;
                modalHotelBuilt = false;
                modalWifi = false;
                modalBuffet = false;
                modalParking = false;
                modalPool = false;
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
                        HotelBuilt = modalHotelBuilt,
                        WifiServiceAvailable = modalWifi,
                        BuffetServiceAvailable = modalBuffet,
                        ParkingServiceAvailable = modalParking,
                        PoolServiceAvailable = modalPool
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
                else if (modalTeamId != parcelProperty.Team.Id
                    || modalHotelBuilt != parcelProperty.HotelBuilt
                    || modalWifi != parcelProperty.WifiServiceAvailable
                    || modalBuffet != parcelProperty.BuffetServiceAvailable
                    || modalParking != parcelProperty.ParkingServiceAvailable
                    || modalPool != parcelProperty.PoolServiceAvailable)
                {
                    // Edit parcel property
                    var editedParcelProperty = parcelProperty.ShallowCopy();
                    editedParcelProperty.TeamId = modalTeamId;
                    editedParcelProperty.HotelBuilt = modalHotelBuilt;
                    editedParcelProperty.WifiServiceAvailable = modalWifi;
                    editedParcelProperty.BuffetServiceAvailable = modalBuffet;
                    editedParcelProperty.ParkingServiceAvailable = modalParking;
                    editedParcelProperty.PoolServiceAvailable = modalPool;

                    await gameManager.UpdateParcelProperty(editedParcelProperty);
                }
                // Otherwise, nothing changed. Don't do anything
            }

            HideModal();
        }
    }
}