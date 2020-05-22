using GameON.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameON.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<DeveloperEntity> Developers { get; set; }

        public DbSet<GameListEntity> GameLists { get; set; }

        public DbSet<GenreEntity> Genres { get; set; }

        public DbSet<ReviewEntity> Reviews { get; set; }

        public DbSet<PlatformEntity> Platforms { get; set; }

        public DbSet<VideoGameEntity> VideoGames { get; set; }
    }
}
