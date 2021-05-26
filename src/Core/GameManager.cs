using Funpoly.Data.Models;
using Funpoly.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funpoly.Core
{
    // Class dedicated to the coordination of the clients. When an update occurs, all connected clients get notified to update their pages
    public class GameManager : IGameManager
    {
        private readonly IGameRepository gameRepository;

        public GameManager(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
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
            .Include(game => game.Teams));
            await NotifyClientsAsync();
        }

        public async Task StartGame()
        {
            game.Status = GameStatus.OnGoing;
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
            team.Postcards = new List<Postcard>();
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
    }
}