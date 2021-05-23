using Funpoly.Data.Models;
using System;
using System.Threading.Tasks;

namespace Funpoly.Services
{
    // Class dedicated to the coordination of the clients. When an update occurs, all connected clients get notified to update their pages
    public class GameManager : IGameManager
    {
        private Game game;

        private Game GameProperty
        {
            get { return game; }
            set { game = value; }
        }

        public event Func<Task> OnChange;

        public async Task NotifyClients()
        {
            await OnChange?.Invoke();
        }

        public Game GetGame()
        {
            return GameProperty;
        }
    }
}