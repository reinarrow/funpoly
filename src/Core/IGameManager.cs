using Funpoly.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funpoly.Core
{
    public interface IGameManager
    {
        Game GetGame();

        event Func<Task> OnChange;

        Task NotifyClientsAsync();

        Task LoadGameById(int id);

        Task StartGame();

        Task AddTeam(Team team);

        Task UpdateTeam(Team team);

        Task UpdateTeamCash(Team team, decimal newCash);

        Task<List<Parcel>> GetContinentParcelsWithProperties(int ContinentId);

        Task CreateParcelProperty(ParcelProperty parcelProperty);

        Task UpdateParcelProperty(ParcelProperty parcelProperty);

        Task RemoveParcelProperty(ParcelProperty parcelProperty);
    }
}