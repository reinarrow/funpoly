using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Funpoly.Services
{
    public class BroadcastHub : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}