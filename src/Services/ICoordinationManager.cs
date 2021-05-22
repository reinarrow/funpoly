using System;
using System.Threading.Tasks;

namespace Funpoly.Services
{
    public interface ICoordinationManager
    {
        event Func<Task> OnChange;

        Task NotifyClients();
    }
}