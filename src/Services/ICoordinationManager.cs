using System;
using System.Threading.Tasks;

namespace Funpoly.Services
{
    public interface ICoordinationManager
    {
        Func<Task> OnChange { get; set; }

        Task NotifyClients();
    }
}