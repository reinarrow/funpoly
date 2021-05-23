using Funpoly.Data.Models;
using System.Threading.Tasks;

namespace Funpoly.Data.Repositories.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {  
        Task<Game> GetAsync();
    }
}