using Funpoly.Data.Models;
using System;
using System.Threading.Tasks;

namespace Funpoly.Services
{
    public interface IGameManager
    {
        Game GetGame();

        event Func<Task> OnChange;

        Task NotifyClientsAsync();
        Task LoadGameById(int id);
    }
}