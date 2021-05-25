using Funpoly.Data.Models;
using Funpoly.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Funpoly.Services
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
    }
}