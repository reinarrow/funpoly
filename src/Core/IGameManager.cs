using Funpoly.Data.Models;
using System;
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

        Task AddTeam(Team team);

        Task UpdateTeam(Team team);
    }
}