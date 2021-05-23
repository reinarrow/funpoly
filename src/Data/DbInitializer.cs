using Funpoly.Data.Models;
using Funpoly.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Funpoly.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            var boardSquareRepository = serviceProvider.GetRequiredService<IRepository<BoardSquare>>();
            var continentRepository = serviceProvider.GetRequiredService<IRepository<Continent>>();
            var gameRepository = serviceProvider.GetRequiredService<IGameRepository>();
            var transportRepository = serviceProvider.GetRequiredService<IRepository<Transport>>();

            //Migrations

            applicationDbContext.Database.Migrate();

            // Seed data

            // - BoardSquares

            if (boardSquareRepository.CheckIsEmptyAsync().Result)
            {
                List<BoardSquare> board = new();
                AppendBoardSquareListByType(board, SquareTypes.Parcel, 2, 4, 7, 9, 11, 13, 16, 18, 20, 22, 25, 27, 29, 31, 34, 36);
                AppendBoardSquareListByType(board, SquareTypes.Game, 3, 5, 14, 21, 24, 32);
                AppendBoardSquareListByType(board, SquareTypes.Surprise, 6, 12, 17, 26, 30, 35);
                AppendBoardSquareListByType(board, SquareTypes.Tax, 15, 33);
                AppendBoardSquareListByType(board, SquareTypes.Dices, 8);
                AppendBoardSquareListByType(board, SquareTypes.Teleport, 23);
                AppendBoardSquareListByType(board, SquareTypes.Border, 1);
                AppendBoardSquareListByType(board, SquareTypes.Prize, 10);
                AppendBoardSquareListByType(board, SquareTypes.Auction, 19);
                AppendBoardSquareListByType(board, SquareTypes.Prison, 28);
                boardSquareRepository.AddRange(board);
            }

            // - Continents, parcels and postcards
            if (continentRepository.CheckIsEmptyAsync().Result)
            {
                List<Continent> continents = new()
                {
                    new Continent
                    {
                        Name = "África",
                        Parcels = new List<Parcel>
                        {
                            new Parcel {
                                Name = "Pirámides de Giza, El Cairo",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 2 //TODO: Check
                            },
                            new Parcel
                            {
                                Name = "Valle de los Reyes, Luxor",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 4 //TODO: Check
                            },
                            new Parcel
                            {
                                Name = "Serengeti, Tanzania",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 7 //TODO: Check
                            },
                            new Parcel
                            {
                                Name = "El Sáhara, Marruecos",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 9 //TODO: Check
                            }
                        }
                    },
                    new Continent
                    {
                        Name = "Europa",
                        Parcels = new List<Parcel>
                        {
                            new Parcel
                            {
                                Name = "Coliseo Romano, Italia",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 11 //TODO: Check
                            },
                            new Parcel
                            {
                                Name = "Partenón, Atenas",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 13 //TODO: Check
                            },
                            new Parcel
                            {
                                Name = "Torre Eiffel, Paris",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 16 //TODO: Check
                            },
                            new Parcel
                            {
                                Name = "Catedral de Santa María, Sevilla",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 18 //TODO: Check
                            }
                        }
                    },
                    new Continent
                    {
                        Name = "Asia",
                        Parcels = new List<Parcel>
                        {
                            new Parcel
                            {
                                Name = "Muralla China, Pekín",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 20 //TODO: Check
                            },
                            new Parcel
                            {
                                Name = "Monte Fuji, Japón",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 22 //TODO: Check
                            },
                            new Parcel
                            {
                                Name = "Taj Mahal, India",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 25 //TODO: Check
                            },
                            new Parcel
                            {
                                Name = "Petra, Jordania",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 27 //TODO: Check
                            }
                        }
                    },
                    new Continent
                    {
                        Name = "América",
                        Parcels = new List<Parcel>
                        {
                            
                            new Parcel { Name = "Machu Picchu, Perú", Price = 0, RawTax = 0, HotelTax = 0, HotelBuilt = false, Postcard = new(), BoardSquareId = 29 },
                            new Parcel
                            {
                                Name = "Isla de Pascua, Chile",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 31 //TODO: Check
                            },
                            new Parcel
                            {
                                Name = "Cañón del Colorado, Arizona",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 34 //TODO: Check
                            },
                            new Parcel
                            {
                                Name = "Parque de Yellowstone, EEUU",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 36 //TODO: Check
                            }
                        }
                    }
                };

                continentRepository.AddRange(continents);
            }

            // - Transports
            if (transportRepository.CheckIsEmptyAsync().Result)
            {
                transportRepository.AddRange(new List<Transport>
                {
                    new Transport { Name = "Avión", Dices = 2, Substraction = 0 },
                    new Transport { Name = "Globo", Dices = 2, Substraction = 1 },
                    new Transport { Name = "Tren", Dices = 1, Substraction = 0 },
                    new Transport { Name = "Barco", Dices = 1, Substraction = 0 },
                    new Transport { Name = "Elefante", Dices = 1, Substraction = 1 },
                });
            }

            if (webHostEnvironment.IsDevelopment())
            {
                // - Game
                if (gameRepository.CheckIsEmptyAsync().Result)
                {
                    gameRepository.Add(new Game
                    {
                        Status = GameStatus.NotStarted,
                        Name = "Game 1",
                        Teams = new List<Team>
                        {
                            new Team
                            {
                                Name = "Chicago me limpio",
                                Turn = 1,
                                Cash = 1000, //TODO: Check
                                Color = "#FF0000",
                                BoardSquareId = 1,
                                Players = new List<Player>
                                {
                                    new Player { Name = "Pepa", Captain = true },
                                    new Player { Name = "Lucas", Captain = false },
                                    new Player { Name = "Rodolfa", Captain = false },
                                    new Player { Name = "Agustín", Captain = false }
                                }
                            },
                            new Team
                            {
                                Name = "Nottingham Prisa",
                                Turn = 2,
                                Cash = 1000, //TODO: Check
                                Color = "#00FF00",
                                BoardSquareId = 1,
                                Players = new List<Player>
                                {
                                    new Player { Name = "Carmen", Captain = true },
                                    new Player { Name = "Pepe", Captain = false },
                                    new Player { Name = "Juan", Captain = false },
                                    new Player { Name = "Melinda", Captain = false }
                                }
                            },
                            new Team
                            {
                                Name = "Estudiabaantes",
                                Turn = 3,
                                Cash = 1000, //TODO: Check
                                Color = "#0000FF",
                                BoardSquareId = 1,
                                Players = new List<Player>
                                {
                                    new Player { Name = "Carlos", Captain = true },
                                    new Player { Name = "Josefina", Captain = false },
                                    new Player { Name = "Josh", Captain = false },
                                    new Player { Name = "Tina", Captain = false }
                                }
                            },
                            new Team
                            {
                                Name = "Esfinter de Milán",
                                Turn = 4,
                                Cash = 1000, //TODO: Check
                                Color = "#FFFFFF",
                                BoardSquareId = 1,
                                Players = new List<Player>
                                {
                                    new Player { Name = "Phil", Captain = true },
                                    new Player { Name = "Magdalena", Captain = false },
                                    new Player { Name = "Paula", Captain = false },
                                    new Player { Name = "Yen", Captain = false }
                                }
                            }
                        }
                    });
                }
            }
        }

        public static void AppendBoardSquareListByType(
            List<BoardSquare> boardSquares,
            SquareTypes squareType,
            params int[] squareIds)
        => boardSquares.AddRange(squareIds.Select(squareId => new BoardSquare { Id = squareId, Type = squareType }));

    }
}