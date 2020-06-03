using GameON.Common.Enums;
using GameON.Web.Data.Entities;
using GameON.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;

            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoleAsync();
            await CheckPlatformsAsync();
            await CheckGenresAsync();
            await CheckDevelopersAsync();
            await CheckVideoGamesAsync();
            await CheckUserAsync("Hector", "Masso", "hectormasso@hotmail.com", "123456", UserType.Admin);
        }

        private async Task CheckRoleAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());

        }

        private async Task CheckDevelopersAsync()
        {
            if (!_context.Developers.Any())
            {
                AddDeveloper("Nintendo");
                AddDeveloper("Rockstar Studios");
                AddDeveloper("Ubisoft");
                AddDeveloper("Epic Games");
                AddDeveloper("Valve Corporation");
                AddDeveloper("Activision");
                AddDeveloper("From Software");
                AddDeveloper("Sega Games");
                AddDeveloper("Naughty Dog");
                AddDeveloper("CD Projekt RED");
                AddDeveloper("EA");
                AddDeveloper("Mojang AB");
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckGenresAsync()
        {
            if (!_context.Genres.Any())
            {
                AddGenre("Action");
                AddGenre("Adventure");
                AddGenre("Fighting");
                AddGenre("Platform");
                AddGenre("Puzzle");
                AddGenre("Racing");
                AddGenre("Shooter");
                AddGenre("RPG");
                AddGenre("Sports");
                AddGenre("Open World");
                AddGenre("Sandbox");
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckPlatformsAsync()
        {
            if (!_context.Platforms.Any())
            {
                AddPlatform("Nintendo Switch");
                AddPlatform("Xbox One");
                AddPlatform("PS4");
                AddPlatform("PS3");
                AddPlatform("Xbox 360");
                AddPlatform("Nintendo Wii");
                AddPlatform("SNES");
                AddPlatform("NES");
                AddPlatform("PC");
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckVideoGamesAsync()
        {
            if (!_context.VideoGames.Any())
            {

                _context.VideoGames.Add(new VideoGameEntity
                {
                    Name = "Assassin's Creed: Rogue",
                    PicturePath = $"/images/VideoGames/AssassinsCreed.jpg",
                    ReleaseDate = new DateTime(2014, 11, 11),
                    Score = 4,
                    Synopsis = "Shay Patrick Cormac (Steven Piovesan) is a new recruit to the Colonial Brotherhood of Assassins whose potential is offset by his insubordination. While training with the North Atlantic chapter under the Assassin Mentor Achilles Davenport (Roger Aaron Brown), he encounters the Assassin Adéwalé (Tristan D. Lalla), who brings news that the Haitian city of Port-au-Prince has been devastated by an earthquake during the search for a Precursor temple containing a Piece of Eden. With his experience captaining ships—including the recently acquired Morrigan—Cormac is enlisted in an investigation into Templar interests in a Precursor artifact and manuscript that are linked to the temple. ",
                    Developers = new List<VideoGameDeveloperEntity>
                    {
                        new VideoGameDeveloperEntity
                        {
                            Developer = _context.Developers.FirstOrDefault(t=>t.Name == "Ubisoft"),
                        }
                    },
                    Genres = new List<VideoGameGenreEntity>
                    {
                        new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "Action")
                        },
                         new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "Adventure")
                        },
                        new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "RPG"),
                        }
                    },
                    Platforms = new List<VideoGamePlatformEntity>
                    {
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PS4")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "Xbox One")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PC")
                        }
                    }
                });

                _context.VideoGames.Add(new VideoGameEntity
                {
                    Name = "Red Dead Redemption 2",
                    PicturePath = $"/images/VideoGames/RedDead.jpg",
                    ReleaseDate = new DateTime(2018, 10, 26),
                    Score = 5,
                    Synopsis = "The world of Red Dead Redemption 2 spans five fictitious U.S. states. The states of New Hanover, Ambarino and Lemoyne are new to the series, and are located to the immediate north and east of Red Dead Redemption's world, whilst the states of New Austin and West Elizabeth return from Red Dead Redemption. The states are centered on the San Luis and Lannahechee Rivers and the shores of Flat Iron Lake. Ambarino is a mountain wilderness, with the largest settlement being a Native American reservation; New Hanover is a wide valley that has become a hub of industry and is home to the cattle town of Valentine; and Lemoyne is composed of bayous and plantations resembling Louisiana, and is home to the Southern town of Rhodes and the former French colony of Saint Denis, analogous to New Orleans. West Elizabeth consists of wide plains, dense forests, and the modern town of Blackwater. This region has been expanded from the original Red Dead Redemption to include a vast northern portion containing the small town of Strawberry. New Austin is an arid desert region centered on the frontier towns of Armadillo and Tumbleweed, also featured in the original game. Parts of New Austin and West Elizabeth have been redesigned to reflect the earlier time; for example, Blackwater is still under development, while Armadillo has become a ghost town because of a cholera outbreak.",
                    Developers = new List<VideoGameDeveloperEntity>
                    {
                        new VideoGameDeveloperEntity
                        {
                            Developer = _context.Developers.FirstOrDefault(t=>t.Name == "Rockstar Studios"),
                        }
                    },
                    Genres = new List<VideoGameGenreEntity>
                    {
                        new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "Action")
                        },
                         new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "Adventure")
                        }
                    },
                    Platforms = new List<VideoGamePlatformEntity>
                    {
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PS4")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "Xbox One")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PC")
                        }
                    }
                });

                _context.VideoGames.Add(new VideoGameEntity
                {
                    Name = "Super Mario Odyssey",
                    PicturePath = $"/images/VideoGames/MarioOdyssey.jpg",
                    ReleaseDate = new DateTime(2017, 10, 27),
                    Score = 5,
                    Synopsis = "Mario se enfrenta contra Bowser dentro de su nave. El villano ya estaba vestido y preparado para casarse con la Princesa Peach (a quien ya la tenía secuestrada). Después de una fuerte pelea, Bowser le lanza su sombrero a Mario para tirarlo de la nave (el primer golpe solo le tira el gorro a Mario, pero luego el sombrero regresa con efecto boomerang, y logra tirarlo de la nave). Después Bowser pisotea el gorro que se le había caído a Mario y se va del Reino Champiñón con la princesa, permitiendo que las hélices de su nave trituren el gorro. La escena termina con un pequeño alienígena con forma de sombrero atrapando un pedazo del gorro que lleva la letra M de Mario, y mirando la nave de Bowser alejándose. Tras toda la aventura, Mario enfrenta a Bowser en plena boda. Cuando acaba la pelea, Bowser y Mario intentan seducir a Peach. La princesa los rechaza a los dos y activa la nave Oddysey. En el último momento Mario salta sobre Bowser, que se queda en la Luna. ",
                    Developers = new List<VideoGameDeveloperEntity>
                    {
                        new VideoGameDeveloperEntity
                        {
                            Developer = _context.Developers.FirstOrDefault(t=>t.Name == "Nintendo"),
                        }
                    },
                    Genres = new List<VideoGameGenreEntity>
                    {
                        new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "Platform")
                        },
                        new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "Open World")
                        }
                    },
                    Platforms = new List<VideoGamePlatformEntity>
                    {
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "Nintendo Switch")
                        }
                    }
                });

                _context.VideoGames.Add(new VideoGameEntity
                {
                    Name = "Fifa 20",
                    PicturePath = $"/images/VideoGames/Fifa20.jpg",
                    ReleaseDate = new DateTime(2019, 09, 27),
                    Score = 3,
                    Synopsis = "FIFA 20 es un videojuego de simulación de fútbol desarrollado por EA Sports, como parte de la serie FIFA de Electronic Arts. Está disponible en las plataformas de PlayStation 4, Xbox One, Microsoft Windows y Nintendo Switch (version Legacy). EA Sports lanzó la demo el 10 de septiembre de ese año y el juego el día 27 del mismo. Es el primer juego de la franquicia en no estar disponible para PS3 y Xbox 360, siendo FIFA 19 el último en salir para estas consolas. ",
                    Developers = new List<VideoGameDeveloperEntity>
                    {
                        new VideoGameDeveloperEntity
                        {
                            Developer = _context.Developers.FirstOrDefault(t=>t.Name == "EA"),
                        }
                    },
                    Genres = new List<VideoGameGenreEntity>
                    {
                        new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "Sports")
                        }
                    },
                    Platforms = new List<VideoGamePlatformEntity>
                    {
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PS4")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "Xbox One")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PC")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "Nintendo Switch")
                        }
                    }
                });

                _context.VideoGames.Add(new VideoGameEntity
                {
                    Name = "The Witcher 3: Wild Hunt",
                    PicturePath = $"/images/VideoGames/TW3.jpg",
                    ReleaseDate = new DateTime(2015, 05, 19),
                    Score = 6,
                    Synopsis = "La historia se centra en el personaje Geralt de Rivia, quien recibe una carta de su amante Yennefer de Vengerberg diciendo que necesita localizarlo lo antes posible. Geralt, después de encontrar a su amante, aprende que Ciri, hija de Calenthe y exalumna del mismo personaje, es buscada por La Cacería Salvaje, un grupo antiguo de espectros que están liderados por el Rey de La Cacería Salvaje. Tras varios sucesos que llevan al personaje principal a buscar a Ciri en la gran ciudad de Novigrado, en las Islas Skellige y en las tierras de Velen, Geralt, aprende que La Cacería Salvaje, busca una manera de que se cumpla la Profecía de Ithlinne, la cual dice que el universo será destruido por el Frío Blanco. Ciri, debido a que es hija de la sangre vieja, es la única que puede destruir esta profecía y salvar al mundo. ",
                    Developers = new List<VideoGameDeveloperEntity>
                    {
                        new VideoGameDeveloperEntity
                        {
                            Developer = _context.Developers.FirstOrDefault(t=>t.Name == "CD Projekt RED"),
                        }
                    },
                    Genres = new List<VideoGameGenreEntity>
                    {
                        new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "Action")
                        },
                         new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "Adventure")
                        },
                        new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "RPG"),
                        }
                    },
                    Platforms = new List<VideoGamePlatformEntity>
                    {
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PS4")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "Xbox One")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PC")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "Nintendo Switch")
                        }
                    }
                });

                _context.VideoGames.Add(new VideoGameEntity
                {
                    Name = "Sekiro: Shadows Die Twice",
                    PicturePath = $"/images/VideoGames/Sekiro.jpg",
                    ReleaseDate = new DateTime(2019, 03, 22),
                    Score = 4,
                    Synopsis = "En un reinventado período Sengoku de finales del siglo XVI en Japón, el señor de la guerra Isshin Ashina organizó un golpe sangriento y tomó el control de la tierra de Ashina del Ministerio del Interior. Durante este tiempo, un shinobi errante llamado Ukonzaemon Usui adoptó a un huérfano sin nombre, conocido por muchos como Owl, quien nombró al niño Lobo y lo entrenó en los caminos del shinobi. Dos décadas más tarde, el clan Ashina está al borde del colapso debido a una combinación del ahora anciano Isshin que se enfermó y los enemigos del clan se fueron acercando constantemente por todos lados. Desesperado por salvar a su clan, el nieto de Isshin, Genichiro, buscó al Divino Heredero Kuro para que pueda usar la Herencia del Dragón del niño para crear un ejército inmortal. ",
                    Developers = new List<VideoGameDeveloperEntity>
                    {
                        new VideoGameDeveloperEntity
                        {
                            Developer = _context.Developers.FirstOrDefault(t=>t.Name == "From Software"),
                        }
                    },
                    Genres = new List<VideoGameGenreEntity>
                    {
                        new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "Action")
                        },
                         new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "Adventure")
                        }
                    },
                    Platforms = new List<VideoGamePlatformEntity>
                    {
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PS4")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "Xbox One")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PC")
                        }
                    }
                });

                _context.VideoGames.Add(new VideoGameEntity
                {
                    Name = "Minecraft",
                    PicturePath = $"/images/VideoGames/Minecraft.jpg",
                    ReleaseDate = new DateTime(2019, 11, 18),
                    Score = 5,
                    Synopsis = "Minecraft es un juego de mundo abierto, por lo que no posee un objetivo específico, permitiéndole al jugador una gran libertad en cuanto a la elección de su forma de jugar. A pesar de ello, el juego posee un sistema de logros.4​ El modo de juego predeterminado es en primera persona, aunque los jugadores tienen la posibilidad de cambiarlo a tercera persona.​ El juego se centra en la colocación y destrucción de bloques, siendo que este se compone de objetos tridimensionales cúbicos, colocados sobre un patrón de rejilla fija. Estos cubos o bloques representan principalmente distintos elementos de la naturaleza, como tierra, piedra, minerales, troncos, entre otros.​ Los jugadores son libres de desplazarse por su entorno y modificarlo mediante la creación, recolección y transporte de los bloques que componen al juego, los cuales solo pueden ser colocados respetando la rejilla fija del juego",
                    Developers = new List<VideoGameDeveloperEntity>
                    {
                        new VideoGameDeveloperEntity
                        {
                            Developer = _context.Developers.FirstOrDefault(t=>t.Name == "Mojang AB"),
                        }
                    },
                    Genres = new List<VideoGameGenreEntity>
                    {
                        new VideoGameGenreEntity
                        {
                            Genre = _context.Genres.FirstOrDefault(t=>t.Genre == "Sandbox")
                        }
                    },
                    Platforms = new List<VideoGamePlatformEntity>
                    {
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PS4")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "Xbox One")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PC")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "Xbox 360")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "PS4")
                        },
                        new VideoGamePlatformEntity
                        {
                            Platform = _context.Platforms.FirstOrDefault(t=>t.Name == "Nintendo Switch")
                        }
                    }
                });

                await _context.SaveChangesAsync();
            }
        }

        private void AddDeveloper(string name)
        {
            _context.Developers.Add(new DeveloperEntity
            {
                Name = name
            });
        }

        private void AddGenre(string name)
        {
            _context.Genres.Add(new GenreEntity
            {
                Genre = name
            });
        }

        private void AddPlatform(string name)
        {
            _context.Platforms.Add(new PlatformEntity
            {
                Name = name
            });
        }

        private async Task<UserEntity> CheckUserAsync(

          string firstName,
          string lastName,
          string email,
          string document,
          UserType userType)
        {
            UserEntity user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    VideoGame = _context.VideoGames.FirstOrDefault(),
                    Email = email,
                    UserName = email,
                    Document = document,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }

            return user;
        }

    }
}
