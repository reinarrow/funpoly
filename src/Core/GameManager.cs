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
        private readonly IRepository<Settings> settingsRepository;

        public GameManager(IRepository<Game> gameRepository
            , IRepository<Team> teamRepository
            , IRepository<Parcel> parcelRepository
            , IRepository<ParcelProperty> parcelPropertyRepository
            , IRepository<Postcard> postcardRepository
            , IRepository<PostcardTeam> postcardTeamRepository
            , IRepository<Settings> settingsRepository)
        {
            this.gameRepository = gameRepository;
            this.teamRepository = teamRepository;
            this.parcelRepository = parcelRepository;
            this.parcelPropertyRepository = parcelPropertyRepository;
            this.postcardRepository = postcardRepository;
            this.postcardTeamRepository = postcardTeamRepository;
            this.settingsRepository = settingsRepository;

            this.settings = settingsRepository.GetAll().SingleOrDefault();
        }

        private Game game;
        private Settings settings;

        private Game GameProperty
        {
            get { return game; }
            set { game = value; }
        }

        private Settings SettingsProperty
        {
            get { return settings; }
            set { settings = value; }
        }

        public event Func<Task> OnChange;

        public async Task NotifyClientsAsync()
        {
            await OnChange?.Invoke();
        }

        public event Func<decimal, Task> OnBankPayment;

        /// <summary>
        ///     Notify banker about payment done to bank
        /// </summary>
        public async Task NotifyBankerAsync(decimal amount)
        {
            await OnBankPayment?.Invoke(amount);
        }

        public event Func<Team, Team, decimal, Task> OnTeamPayment;

        /// <summary>
        ///     Notify banker about payment done to bank
        /// </summary>
        public async Task NotifyTeamPaymentAsync(Team payingTeam, Team receivingTeam, decimal amount)
        {
            await OnTeamPayment?.Invoke(payingTeam, receivingTeam, amount);
        }

        public event Func<int, string, Task> OnSurpriseCard;

        public async Task SendSurpriseCardToTeamAsync(int teamId, string text)
        {
            await OnSurpriseCard?.Invoke(teamId, text);
        }

        public Game GetGame()
        {
            return GameProperty;
        }

        public Settings GetSettings()
        {
            return SettingsProperty;
        }

        public async Task SetSettingsAsync(Settings settings)
        {
            this.settings = settings;
            await settingsRepository.UpdateAsync(settings);
            await NotifyClientsAsync();
        }

        public async Task LoadGameById(int id)
        {
            game = await gameRepository.GetByIdAsync(id, game => game
            .Include(game => game.Teams.OrderBy(t => t.Turn)));

            // If game status is not started, change it to teams config to go to configuration page
            if (game.Status == GameStatus.NotStarted)
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
            team.Cash = settings.InitialCash;
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

            await gameRepository.UpdateAsync(game);

            // Do not write into database yet. Wait for game to start
            await NotifyClientsAsync();
        }

        public async Task<int> GetTeamId(string name)
        {
            return game.Teams.SingleOrDefault(t => t.Name == name).Id;
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

        public async Task PayToTeam(Team payingTeam, Team receivingTeam, decimal amount)
        {
            // Paying team is null when the payment is made by the banker
            if (payingTeam != null)
            {
                // Substract quantity from paying team
                await UpdateTeamCash(payingTeam, payingTeam.Cash - amount);
            }

            // Add quantity to receiving team
            await UpdateTeamCash(receivingTeam, receivingTeam.Cash + amount);

            // Notify team about payment
            NotifyTeamPaymentAsync(payingTeam, receivingTeam, amount);
        }

        public async Task PayToBank(Team team, decimal amount)
        {
            ;
            await UpdateTeamCash(team, team.Cash - amount);

            // Notify banker to decide if money should go to lottery
            await NotifyBankerAsync(amount);
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

        public async Task AddToLotteryPrize(decimal quantity)
        {
            game.LotteryPrize += quantity;
            await gameRepository.UpdateAsync(game);

            await NotifyClientsAsync();
        }

        public async Task GiveLotteryPrizeToTeam(int teamId)
        {
            Team team = game.Teams.FirstOrDefault(t => t.Id == teamId);

            team.Cash += game.LotteryPrize;
            await teamRepository.UpdateAsync(team);
            await NotifyTeamPaymentAsync(null, team, game.LotteryPriz);

            game.LotteryPrize = 0;
            await gameRepository.UpdateAsync(game);

            await NotifyClientsAsync();
        }

        public async Task RegisterTeamLap(int teamId, int travelDays, int newTransportId)
        {
            Team team = game.Teams.FirstOrDefault(t => t.Id == teamId);

            // Increment travel days
            team.Days += travelDays;

            // Pay the lap tax and notify team
            team.Cash += settings.LapPaymentAmount;
            await NotifyTeamPaymentAsync(null, team, settings.LapPaymentAmount);

            // Assign the new transport
            team.TransportId = newTransportId;

            await teamRepository.UpdateAsync(team);
            await NotifyClientsAsync();
        }

        public async Task FinePerSpeedLimit(int teamId, bool fine, bool reprimand)
        {
            Team team = game.Teams.FirstOrDefault(t => t.Id == teamId);

            if (fine)
            {
                team.Cash -= settings.SpeedFineTax;
            }

            if (reprimand)
            {
                team.ConsecutiveSixes += 1;
                if (team.ConsecutiveSixes >= settings.SpeedReprimandCount) team.ConsecutiveSixes = 0;
            }

            await teamRepository.UpdateAsync(team);
            await NotifyClientsAsync();
        }
    }
}