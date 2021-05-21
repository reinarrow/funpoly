using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Funpoly.Data;
using Funpoly.Services;
using Funpoly.Data.Repositories.Interfaces;
using Funpoly.Data.Repositories;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.ResponseCompression;

namespace Funpoly
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IBoardSquareRepository, BoardSquareRepository>();
            services.AddTransient<IContinentRepository, ContinentRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IParcelRepository, ParcelRepository>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IPostcardRepository, PostcardRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<ITransportRepository, TransportRepository>();

            services.AddSingleton<ICoordinationManager, CoordinationManager>();

            // Connection string host is different from within the app container and the host dev computer (for executing dotnet ef commands)
            var connectionString = Environment.GetEnvironmentVariable("CONTAINER") == "docker" ?
                "Host=funpoly_postgres;Port=5432;Database=funpoly;Username=rw_dev;Password=rw_dev;" :
                "Host=localhost;Port=5432;Database=funpoly;Username=rw_dev;Password=rw_dev;";
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            }, ServiceLifetime.Transient);

            // Cookies
            services.AddBlazoredLocalStorage();   // local storage
            services.AddBlazoredLocalStorage(config =>
                config.JsonSerializerOptions.WriteIndented = true);  // local storage
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}