using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;
using Funpoly.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Funpoly.Core
{
    // Class dedicated to the coordination of the clients. When an update occurs, all connected clients get notified to update their pages
    public class GameManager : IGameManager
    {
        private readonly IRepository<Game> gameRepository;
        private readonly IRepository<Team> teamRepository;
        private readonly IRepository<Parcel> parcelRepository;
        private readonly IRepository<ParcelProperty> parcelPropertyRepository;
        private readonly IRepository<Postcard> postcardRepository;
        private readonly IRepository<PostcardTeam> postcardTeamRepository;

        public GameManager(IRepository<Game> gameRepository
            , IRepository<Team> teamRepository
            , IRepository<Parcel> parcelRepository
            , IRepository<ParcelProperty> parcelPropertyRepository
            , IRepository<Postcard> postcardRepository
            , IRepository<PostcardTeam> postcardTeamRepository)
        {
            this.gameRepository = gameRepository;
            this.teamRepository = teamRepository;
            this.parcelRepository = parcelRepository;
            this.parcelPropertyRepository = parcelPropertyRepository;
            this.postcardRepository = postcardRepository;
            this.postcardTeamRepository = postcardTeamRepository;
        }

        private Game game;

        private Game GameProperty
        {
            get { return game; }
            set { game = value; }
        }

        public event Func<Task> OnChange;

        public async Task NotifyClientsAsync()
        {
            await OnChange?.Invoke();
        }

        public Game GetGame()
        {
            return GameProperty;
        }

        public async Task LoadGameById(int id)
        {
            game = await gameRepository.GetByIdAsync(id, game => game
            .Include(game => game.Teams.OrderBy(t => t.Turn)));

            // If game status is not in TeamsConfig Status, change it
            if (game.Status != GameStatus.TeamsConfig)
            {
                game.Status = GameStatus.TeamsConfig;
                await gameRepository.UpdateAsync(game);
            }
            await NotifyClientsAsync();
        }

        public async Task StartGame()
        {
            game.Status = GameStatus.OnGoing;
            await gameRepository.UpdateAsync(game);
            await NotifyClientsAsync();
        }

        public async Task FinishGame()
        {
            game.Status = GameStatus.Finished;
            await gameRepository.UpdateAsync(game);
            await NotifyClientsAsync();
        }

        public async Task AddTeam(Team team)
        {
            // Fill in the default initial values
            team.Cash = 1500;
            team.Turn = team.Color switch
            {
                "#00B0F0" => 1, // Blue is 1st
                "#C00000" => 2, // Red is 2nd
                "#FFC000" => 3, // Yellow is 3rd
                "#92D050" => 4, // Green is 4th
                _ => throw new NotImplementedException(),
            };

            team.Days = 0;
            team.ConsecutiveSixes = 0;
            team.TurnsInPrison = 0;
            team.GameId = game.Id;
            team.BoardSquareId = 1;
            team.PostcardTeams = new List<PostcardTeam>();
            team.ParcelProperties = new List<ParcelProperty>();

            game.Teams.Add(team);

            // Do not write into database yet. Wait for game to start
            await NotifyClientsAsync();
        }

        public async Task UpdateTeam(Team team)
        {
            var prevTeam = game.Teams.Find(t => t.Id == team.Id);
            prevTeam.Name = team.Name;
            prevTeam.Color = team.Color;

            await NotifyClientsAsync();
        }

        public async Task DeleteTeam(Team team)
        {
            await teamRepository.RemoveAsync(team);

            await NotifyClientsAsync();
        }

        public async Task UpdateTeamCash(Team team, decimal newCash)
        {
            var prevTeam = game.Teams.FirstOrDefault(t => t.Id == team.Id);
            prevTeam.Cash = newCash;

            await teamRepository.UpdateAsync(prevTeam);

            await NotifyClientsAsync();
        }

        public async Task<List<Parcel>> GetContinentParcelsWithProperties(int ContinentId)
        {
            //Get Parcels for given Continent including the properties and teams corresponding to the current game
            var continentParcels = await parcelRepository.GetAllAsync(
                parcel => parcel
                .Where(parcel => parcel.ContinentId == ContinentId)
                .Include(parcel => parcel.ParcelProperties
                              .Where(prop => prop.Team.GameId == game.Id))
                .ThenInclude(prop => prop.Team));
            return continentParcels;
        }

        public async Task CreateParcelProperty(ParcelProperty parcelProperty)
        {
            await parcelPropertyRepository.AddAsync(parcelProperty);

            await NotifyClientsAsync();
        }

        public async Task UpdateParcelProperty(ParcelProperty parcelProperty)
        {
            // EF limitation: Need to remove relationship entities for update to avoid unexpected tracking errors
            parcelProperty.Team = null;
            parcelProperty.Parcel = null;
            await parcelPropertyRepository.UpdateAsync(parcelProperty);

            await NotifyClientsAsync();
        }

        public async Task RemoveParcelProperty(ParcelProperty parcelProperty)
        {
            await parcelPropertyRepository.RemoveAsync(parcelProperty);

            await NotifyClientsAsync();
        }

        public async Task<List<Team>> GetTeamsWithTravelData()
        {
            var teams = await teamRepository.GetAllAsync(t => t.Where(t => t.GameId == game.Id)
            .Include(t => t.Transport).OrderBy(t => t.Turn));

            return teams;
        }

        public async Task UpdateTeamTravelData(int teamId, int travelDays, int? transportId)
        {
            var prevTeam = game.Teams.FirstOrDefault(t => t.Id == teamId);
            prevTeam.Days = travelDays;
            prevTeam.TransportId = transportId;

            await teamRepository.UpdateAsync(prevTeam);
            await NotifyClientsAsync();
        }

        public async Task<List<Postcard>> GetPostcardsByContinent(Continent continent)
        {
            // Get postcards with the corresponding Teams that own it from the current game
            var postcards = await postcardRepository.GetAllAsync(postcard => postcard.Where(postcard => postcard.Parcel.ContinentId == continent.Id)
            .Include(postcard => postcard.Parcel)
            .Include(postcard => postcard.PostcardTeams.Where(pt => pt.Team.GameId == game.Id))
            .ThenInclude(pt => pt.Team));
            return postcards;
        }

        public async Task CreatePostcardTeam(PostcardTeam postcardTeam)
        {
            await postcardTeamRepository.AddAsync(postcardTeam);
            await NotifyClientsAsync();
        }

        public async Task RemovePostcardTeam(PostcardTeam postcardTeam)
        {
            await postcardTeamRepository.RemoveAsync(postcardTeam);
            await NotifyClientsAsync();
        }

        public async Task PayToLotteryPrize(int teamId, decimal quantity)
        {
            Team team = game.Teams.FirstOrDefault(t => t.Id == teamId);
            team.Cash -= quantity;
            await teamRepository.UpdateAsync(team);

            game.LotteryPrize += quantity;
            await gameRepository.UpdateAsync(game);

            await NotifyClientsAsync();
        }

        public async Task GiveLotteryPrizeToTeam(int teamId)
        {
            Team team = game.Teams.FirstOrDefault(t => t.Id == teamId);

            team.Cash += game.LotteryPrize;
            await teamRepository.UpdateAsync(team);

            game.LotteryPrize = 0;
            await gameRepository.UpdateAsync(game);

            await NotifyClientsAsync();
        }
    }
}