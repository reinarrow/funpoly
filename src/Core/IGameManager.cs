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

        Task FinishGame();

        Task AddTeam(Team team);

        Task UpdateTeam(Team team);

        Task DeleteTeam(Team team);

        Task UpdateTeamCash(Team team, decimal newCash);

        Task<List<Parcel>> GetContinentParcelsWithProperties(int ContinentId);

        Task CreateParcelProperty(ParcelProperty parcelProperty);

        Task UpdateParcelProperty(ParcelProperty parcelProperty);

        Task RemoveParcelProperty(ParcelProperty parcelProperty);

        Task<List<Team>> GetTeamsWithTravelData();

        Task UpdateTeamTravelData(int teamId, int travelDays, int? transportId);

        Task<List<Postcard>> GetPostcardsByContinent(Continent continent);

        Task CreatePostcardTeam(PostcardTeam postcardTeam);

        Task RemovePostcardTeam(PostcardTeam postcardTeam);

        Task GiveLotteryPrizeToTeam(int teamId);

        Task PayToLotteryPrize(int teamId, decimal quantity);

        Task RegisterTeamLap(int teamId, int travelDays, int newTransportId);

        Task FinePerSpeedLimit(int teamId, bool fine, bool reprimand);
    }
}