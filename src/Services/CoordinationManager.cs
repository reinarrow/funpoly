using System;
using System.Threading.Tasks;
using System.Linq;
using Funpoly.Data.Repositories.Interfaces;
using Funpoly.Data.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace Funpoly.Services
{
    public class CoordinationManager : Hub, ICoordinationManager
    {
        private HubConnection hubConnection;
        private readonly IGameRepository _gameRepository;
        private readonly NavigationManager _navManager;
        protected IHubContext<CoordinationManager> _context;

        public CoordinationManager(IGameRepository gameRepository, NavigationManager navManager, IHubContext<CoordinationManager> hubContext)
        {
            _gameRepository = gameRepository;
            _navManager = navManager;
            _context = hubContext;

            Task.Run(async () => await InitializeHubConnection());
        }

        // Class dedicated to the coordination of the clientes. When an update occurs, all connected clients get notified to update their pages (by using SignalR-based connection hub)
        // The navigation between pages is done depending on the game status (obtained from GameRepository).
        // If the client is already in the correct page, StateHasChanged is invoked to trigger a refresh
        public async Task InitializeHubConnection()
        {
            // SignalR initialization
            var uri = new Uri("https://localhost/broadcastHub");

            hubConnection = new HubConnectionBuilder()
            .WithUrl(uri, options =>
            {
                //Ignore validating insecure SSL cert
                var httpClientHandler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    }
                };
                options.HttpMessageHandlerFactory = _ => httpClientHandler;
            })
            .Build();

            // Callback for hub notification
            hubConnection.On("Update", async () =>
            {
                await TriggerUpdate();
            });

            await hubConnection.StartAsync();
        }

        public async Task NotifyClients()
        {
            await _context.Clients.All.SendAsync("Update");
        }

        public async Task UpdateGameStatus(GameStatus newStatus)
        {
            var game = await _gameRepository.GetAsync();
            game.Status = newStatus;
            await _gameRepository.UpdateAsync(game);

            // Notify clients about game status change
            await NotifyClients();
        }

        public async Task TriggerUpdate()
        {
            // Game status can be "NotStarted", "TeamsConfig" or "OnGoing". Read from database and navigate to corresponding blazor page
            var game = await _gameRepository.GetAsync();

            string targetUri = "/";
            if (game.Status == GameStatus.TeamsConfig)
            {
                targetUri = "/teams";
            }
            else if (game.Status == GameStatus.OnGoing)
            {
                targetUri = "/game";
            }

            if (_navManager.BaseUri != targetUri)
            {
                _navManager.NavigateTo(targetUri);
            }
            else
            {
                // Cause pages to re-render
                //InvokeAsync(() => StateHasChanged());
            }
        }
    }
}