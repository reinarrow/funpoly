using System;
using System.Threading.Tasks;

namespace Funpoly.Services
{
    // Class dedicated to the coordination of the clients. When an update occurs, all connected clients get notified to update their pages
    public class CoordinationManager : ICoordinationManager
    {
        public Func<Task> OnChange { get; set; }

        public async Task NotifyClients()
        {
            await OnChange();
        }
    }
}