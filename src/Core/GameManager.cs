using System;
using System.Threading.Tasks;

namespace Funpoly.Services
{
    // Class dedicated to the coordination of the clients. When an update occurs, all connected clients get notified to update their pages
    public class GameManager : IGameManager
    {
        public event Func<Task> OnChange;

        public async Task NotifyClients()
        {
            await OnChange?.Invoke();
        }
    }
}