using System;
using System.Threading.Tasks;

namespace Funpoly.Services
{
    public interface IGameManager
    {
        event Func<Task> OnChange;

        Task NotifyClients();
    }
}