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
            var gameRepository = serviceProvider.GetRequiredService<IRepository<Game>>();
            var transportRepository = serviceProvider.GetRequiredService<IRepository<Transport>>();
            var surpriseCardRepository = serviceProvider.GetRequiredService<IRepository<SurpriseCard>>();

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
                            new Parcel
                            {
                                Id = 1,
                                Name = "El Sáhara, Marruecos",
                                Price = 60,
                                HotelPrice = 75,
                                Postcard = new Postcard(),
                                BoardSquareId = 2
                            },
                            new Parcel
                            {
                                Id = 2,
                                Name = "Serengeti, Tanzania",
                                Price = 80,
                                HotelPrice = 75,
                                Postcard = new Postcard(),
                                BoardSquareId = 4
                            },
                            new Parcel
                            {
                                Id = 3,
                                Name = "Pirámides de Giza, El Cairo",
                                Price = 100,
                                HotelPrice = 75,
                                Postcard = new Postcard(),
                                BoardSquareId = 7
                            },
                            new Parcel
                            {
                                Id = 4,
                                Name = "Bosque de baobabs, Madagascar",
                                Price = 120,
                                HotelPrice = 75,
                                Postcard = new Postcard(),
                                BoardSquareId = 9
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
                                Id = 5,
                                Name = "Petra, Jordania",
                                Price = 140,
                                HotelPrice = 100,
                                Postcard = new Postcard(),
                                BoardSquareId = 11
                            },
                            new Parcel
                            {
                                Id = 6,
                                Name = "Monte Fuji, Japón",
                                Price = 160,
                                HotelPrice = 100,
                                Postcard = new Postcard(),
                                BoardSquareId = 16
                            },
                            new Parcel
                            {
                                Id = 7,
                                Name = "Muralla China, Pekín",
                                Price = 180,
                                HotelPrice = 100,
                                Postcard = new Postcard(),
                                BoardSquareId = 13
                            },
                            new Parcel
                            {
                                Id = 8,
                                Name = "Taj Mahal, India",
                                Price = 200,
                                HotelPrice = 100,
                                Postcard = new Postcard(),
                                BoardSquareId = 18
                            }
                        }
                    },

                    new Continent
                    {
                        Name = "América",
                        Parcels = new List<Parcel>
                        {
                            new Parcel
                            {
                                Id = 9,
                                Name = "Machu Picchu, Perú",
                                Price = 220,
                                HotelPrice = 150,
                                Postcard = new(),
                                BoardSquareId = 20
                            },
                            new Parcel
                            {
                                Id = 10,
                                Name = "Cañón del Colorado, Arizona",
                                Price = 240,
                                HotelPrice = 150,
                                Postcard = new Postcard(),
                                BoardSquareId = 22
                            },
                            new Parcel
                            {
                                Id = 11,
                                Name = "Isla de Pascua, Chile",
                                Price = 260,
                                HotelPrice = 150,
                                Postcard = new Postcard(),
                                BoardSquareId = 25
                            },
                            new Parcel
                            {
                                Id = 12,
                                Name = "Parque de Yellowstone, EEUU",
                                Price = 280,
                                HotelPrice = 150,
                                Postcard = new Postcard(),
                                BoardSquareId = 27
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
                                Id = 13,
                                Name = "Partenón, Atenas",
                                Price = 300,
                                HotelPrice = 200,
                                Postcard = new Postcard(),
                                BoardSquareId = 29
                            },
                            new Parcel
                            {
                                Id = 14,
                                Name = "Catedral de Santa María, Sevilla",
                                Price = 340,
                                HotelPrice = 200,
                                Postcard = new Postcard(),
                                BoardSquareId = 31
                            },
                            new Parcel
                            {
                                Id = 15,
                                Name = "Coliseo Romano, Italia",
                                Price = 360,
                                HotelPrice = 200,
                                Postcard = new Postcard(),
                                BoardSquareId = 34
                            },
                            new Parcel
                            {
                                Id = 16,
                                Name = "Torre Eiffel, Paris",
                                Price = 400,
                                HotelPrice = 200,
                                Postcard = new Postcard(),
                                BoardSquareId = 36
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

            // - Surprise cards
            if (surpriseCardRepository.CheckIsEmptyAsync().Result)
            {
                surpriseCardRepository.AddRange(new List<SurpriseCard>
                {
                    new SurpriseCard { Text = "Sabemos de tus intentos de hacerte Instagramer o Youtuber y tus fracasos. Te enviamos 100 € con cariño para que sigas con los estudios. La fama no es lo tuyo..."},
                    new SurpriseCard { Text = "Tu foto de Snapchat que encontramos en la red es digna del premio al ridículo del año. 50 € por la cara."},
                    new SurpriseCard { Text = "Tu vecino te ha denunciado porque tu gato hace caca en sus macetas. Págale 25 € para replantar sus maltrechas flores."},
                    new SurpriseCard { Text = "Somos del programa \"Tú sí que vales\". Hemos recibido la grabación que mandó tu madre para las audiciones y hemos decidido pagarte 100 € para una gira mundial junto a Paquirrín y su hermana. Y así alejaros del país..."},
                    new SurpriseCard { Text = "La revista \"Jardines del Hogar\" le otorga 75 €  por hablarle y cantarle con tanto mimo a las plantas. Para que te puedas pagar un psicólogo."},
                    new SurpriseCard { Text = "Estás invitada a la fiesta de jubilación de la cigüeña que te trajo al mundo y en la invitación indican que hay que vestir de gala. Paga 80 € para comprarte un traje elegante."},
                    new SurpriseCard { Text = "Vocal \"A\"."},
                    new SurpriseCard { Text = "Este vale te da derecho a comer tarta aunque tu grupo sea el primero en caer en bancarrota."},
                    new SurpriseCard { Text = "Donación de 25 € a la asociación protectore de bichitos. No puedes seguir viviendo mientras se aplasta a pobres cucarachitas, arañitas y hormiguitas."},
                    new SurpriseCard { Text = "Donación mundial contra el hambre en el mundo. Todos los equipos donan: El que esté en América 10 €, Europa 15 €, Asia, 20 € y África 25 €."},
                    new SurpriseCard { Text = "Donación de 10 € para montar la asociación que siempre quisiste: \"Peluqueros caninos sin fronteras\" y que los pobres animalitos callejeros no anden con esos pelos por el mundo."},
                    new SurpriseCard { Text = "Ayuda al paquidermo. Cada equipo le paga 10 € al grupo que esté viajando actualmente en Elefante para contribuir con los gastos de alimentación."},
                    new SurpriseCard { Text = "Intercámbiate con el equipo que quieras. Solo cambia de posición, sin hacer lo que te toque en esa casilla no pasar por la frontera."},
                    new SurpriseCard { Text = "Vé a la prisión sin pasar por la frontera."},
                    new SurpriseCard { Text = "Tirada extra. ¡Yuju!"},
                    new SurpriseCard { Text = "Vocal \"E\"."},
                    new SurpriseCard { Text = "Paga 10 € a cada grupo."},
                    new SurpriseCard { Text = "Wifi para tu propiedad."},
                    new SurpriseCard { Text = "Buffet para tu propiedad."},
                    new SurpriseCard { Text = "Parking para tu propiedad."},
                    new SurpriseCard { Text = "Piscina para tu propiedad."},
                    new SurpriseCard { Text = "¡Construye un hotal gratis!"},
                    new SurpriseCard { Text = "Vocal \"I\"."},
                    new SurpriseCard { Text = "Vocal \"O\"."},
                    new SurpriseCard { Text = "Vocal \"U\"."},
                });
            }

            if (webHostEnvironment.IsDevelopment())
            {
                // - Game
                if (gameRepository.CheckIsEmptyAsync().Result)
                {
                    gameRepository.Add(new Game
                    {
                        Status = GameStatus.OnGoing,
                        Name = "Juego 1",
                        CreatedDate = new DateTime(2021, 05, 23),
                        Teams = new List<Team>
                        {
                            new Team
                            {
                                Name = "Chicago me limpio",
                                Turn = 1,
                                Cash = 1500,
                                Color = "#00B0F0",
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
                                Cash = 1500,
                                Color = "#C00000",
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
                                Cash = 1500,
                                Color = "#FFC000",
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
                                Cash = 1500,
                                Color = "#92D050",
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

                    gameRepository.Add(new Game
                    {
                        Status = GameStatus.TeamsConfig,
                        Name = "Juego 2",
                        Teams = new List<Team>
                        {
                            new Team
                            {
                                Name = "Chicago me limpio",
                                Turn = 1,
                                Cash = 1500,
                                Color = "#00B0F0",
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
                                Cash = 1500,
                                Color = "#C00000",
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
                                Cash = 1500,
                                Color = "#FFC000",
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
                                Cash = 1500,
                                Color = "#92D050",
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

        private static void AppendBoardSquareListByType(
            List<BoardSquare> boardSquares,
            SquareTypes squareType,
            params int[] squareIds)
        => boardSquares.AddRange(squareIds.Select(squareId => new BoardSquare { Id = squareId, Type = squareType }));
    }
}