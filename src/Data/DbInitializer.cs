using System;
using Funpoly.Data.Models;
using Funpoly.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Funpoly.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var boardSquareRepository = serviceProvider.GetRequiredService<IBoardSquareRepository>();
            var continentRepository = serviceProvider.GetRequiredService<IContinentRepository>();
            var parcelRepository = serviceProvider.GetRequiredService<IParcelRepository>();
            var playerRepository = serviceProvider.GetRequiredService<IPlayerRepository>();
            var postcardRepository = serviceProvider.GetRequiredService<IPostcardRepository>();
            var teamRepository = serviceProvider.GetRequiredService<ITeamRepository>();
            var transportRepository = serviceProvider.GetRequiredService<ITransportRepository>();

            //Migrations

            applicationDbContext.Database.Migrate();

            // Seed data
            // - BoardSquares
            var currentBoardSquares = boardSquareRepository.GetAll();
            if (currentBoardSquares.Count == 0)
            {
                List<int> parcelSquares = new List<int> { 2, 4, 7, 9, 11, 13, 16, 18, 20, 22, 25, 27, 29, 31, 34, 36 };
                List<int> gameSquares = new List<int> { 3, 5, 14, 21, 24, 32 };
                List<int> surpriseSquares = new List<int> { 6, 12, 17, 26, 30, 35 };
                List<int> taxSquares = new List<int> { 15, 33, };
                int dicesSquare = 8;
                int teleportSquare = 23;
                int borderSquare = 1;
                int prizeSquare = 10;
                int auctionSquare = 19;
                int prisonSquare = 28;

                foreach (int square in parcelSquares)
                {
                    boardSquareRepository.Add(
                        new BoardSquare
                        {
                            Id = square,
                            Type = SquareTypes.Parcel
                        }
                    );
                }

                foreach (int square in gameSquares)
                {
                    boardSquareRepository.Add(
                        new BoardSquare
                        {
                            Id = square,
                            Type = SquareTypes.Game
                        }
                    );
                }

                foreach (int square in surpriseSquares)
                {
                    boardSquareRepository.Add(
                        new BoardSquare
                        {
                            Id = square,
                            Type = SquareTypes.Surprise
                        }
                    );
                }

                foreach (int square in taxSquares)
                {
                    boardSquareRepository.Add(
                        new BoardSquare
                        {
                            Id = square,
                            Type = SquareTypes.Tax
                        }
                    );
                }

                boardSquareRepository.Add(
                    new BoardSquare
                    {
                        Id = dicesSquare,
                        Type = SquareTypes.Dices
                    }
                );

                boardSquareRepository.Add(
                    new BoardSquare
                    {
                        Id = teleportSquare,
                        Type = SquareTypes.Teleport
                    }
                );

                boardSquareRepository.Add(
                    new BoardSquare
                    {
                        Id = borderSquare,
                        Type = SquareTypes.Border
                    }
                );

                boardSquareRepository.Add(
                    new BoardSquare
                    {
                        Id = prizeSquare,
                        Type = SquareTypes.Prize
                    }
                );

                boardSquareRepository.Add(
                    new BoardSquare
                    {
                        Id = auctionSquare,
                        Type = SquareTypes.Auction
                    }
                );

                boardSquareRepository.Add(
                    new BoardSquare
                    {
                        Id = prisonSquare,
                        Type = SquareTypes.Prison
                    }
                );
            }

            // - Continents, parcels and postcards
            var currentContinents = continentRepository.GetAll();
            if (currentContinents.Count == 0)
            {
                List<Continent> continents = new List<Continent>
                {
                    new Continent{
                        Name ="África",
                        Parcels = new List<Parcel>
                        {
                            new Parcel
                            {
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
                    new Continent{Name ="Europa",
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
                    new Continent{Name ="Asia",
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
                    new Continent{Name ="América",
                    Parcels = new List<Parcel>
                        {
                            new Parcel
                            {
                                Name = "Machu Picchu, Perú",
                                Price = 0, //TODO: Fill
                                RawTax = 0, //TODO: Fill
                                HotelTax = 0, //TODO: Fill
                                HotelBuilt = false,
                                Postcard = new Postcard(),
                                BoardSquareId = 29 //TODO: Check
                            },
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

                foreach (var continent in continents)
                {
                    continentRepository.Add(continent);
                }
            }

            // - Transports
            var currentTransports = transportRepository.GetAll();

            if (currentTransports.Count == 0)
            {
                var transports = new List<Transport>
                {
                    new Transport
                    {
                        Name = "Avión",
                        Dices = 2,
                        Substraction = 0
                    },
                    new Transport
                    {
                        Name = "Globo",
                        Dices = 2,
                        Substraction = 1
                    },
                    new Transport
                    {
                        Name = "Tren",
                        Dices = 1,
                        Substraction = 0
                    },
                    new Transport
                    {
                        Name = "Barco",
                        Dices = 1,
                        Substraction = 0
                    },
                    new Transport
                    {
                        Name = "Elefante",
                        Dices = 1,
                        Substraction = 1
                    },
                };

                foreach (var transport in transports)
                {
                    transportRepository.Add(transport);
                }
            }

            //if (!applicationDbContext.Players.Any())
            //{
            //    // Players
            //    applicationDbContext.Players.Add(
            //        new Player
            //        {
            //            Name = "Agustin"
            //        });
            //    applicationDbContext.Players.Add(
            //        new Player
            //        {
            //            Name = "Lucas"
            //        });
            //    applicationDbContext.Players.Add(
            //        new Player
            //        {
            //            Name = "Rodolfa"
            //        });
            //    applicationDbContext.SaveChanges();
            //}
        }
    }
}