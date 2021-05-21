using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funpoly.Data.Models;

namespace Funpoly.Services
{
    public interface ICoordinationManager
    {
        Task InitializeHubConnection();

        Task NotifyClients();

        Task UpdateGameStatus(GameStatus newStatus);

        Task TriggerUpdate();
    }
}