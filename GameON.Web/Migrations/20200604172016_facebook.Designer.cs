﻿// <auto-generated />
using System;
using GameON.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameON.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200604172016_facebook")]
    partial class facebook
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameON.Web.Data.Entities.DeveloperEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.GameListEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserId");

                    b.Property<int?>("VideoGameId");

                    b.Property<int>("status");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VideoGameId");

                    b.ToTable("GameLists");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.GenreEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("Genre")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.PlatformEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.ReviewEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasMaxLength(1500);

                    b.Property<float>("Score");

                    b.Property<string>("UserId");

                    b.Property<int?>("VideoGameId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("VideoGameId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<int>("LoginType");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PicturePath");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("UserType");

                    b.Property<int?>("VideoGameId");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("VideoGameId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.VideoGameDeveloperEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DeveloperId");

                    b.Property<int?>("VideoGameId");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("VideoGameId");

                    b.ToTable("VGDevelopers");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.VideoGameEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PicturePath");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<float>("Score");

                    b.Property<string>("Synopsis")
                        .IsRequired()
                        .HasMaxLength(1500);

                    b.HasKey("Id");

                    b.ToTable("VideoGames");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.VideoGameGenreEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GenreId");

                    b.Property<int?>("VideoGameId");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("VideoGameId");

                    b.ToTable("VGGenres");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.VideoGamePlatformEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PlatformId");

                    b.Property<int?>("VideoGameId");

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.HasIndex("VideoGameId");

                    b.ToTable("VGPlatforms");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.GameListEntity", b =>
                {
                    b.HasOne("GameON.Web.Data.Entities.UserEntity", "User")
                        .WithMany("GameList")
                        .HasForeignKey("UserId");

                    b.HasOne("GameON.Web.Data.Entities.VideoGameEntity", "VideoGame")
                        .WithMany()
                        .HasForeignKey("VideoGameId");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.ReviewEntity", b =>
                {
                    b.HasOne("GameON.Web.Data.Entities.UserEntity", "User")
                        .WithMany("Review")
                        .HasForeignKey("UserId");

                    b.HasOne("GameON.Web.Data.Entities.VideoGameEntity", "VideoGame")
                        .WithMany("Reviews")
                        .HasForeignKey("VideoGameId");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.UserEntity", b =>
                {
                    b.HasOne("GameON.Web.Data.Entities.VideoGameEntity", "VideoGame")
                        .WithMany("Users")
                        .HasForeignKey("VideoGameId");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.VideoGameDeveloperEntity", b =>
                {
                    b.HasOne("GameON.Web.Data.Entities.DeveloperEntity", "Developer")
                        .WithMany()
                        .HasForeignKey("DeveloperId");

                    b.HasOne("GameON.Web.Data.Entities.VideoGameEntity", "VideoGame")
                        .WithMany("Developers")
                        .HasForeignKey("VideoGameId");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.VideoGameGenreEntity", b =>
                {
                    b.HasOne("GameON.Web.Data.Entities.GenreEntity", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId");

                    b.HasOne("GameON.Web.Data.Entities.VideoGameEntity", "VideoGame")
                        .WithMany("Genres")
                        .HasForeignKey("VideoGameId");
                });

            modelBuilder.Entity("GameON.Web.Data.Entities.VideoGamePlatformEntity", b =>
                {
                    b.HasOne("GameON.Web.Data.Entities.PlatformEntity", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformId");

                    b.HasOne("GameON.Web.Data.Entities.VideoGameEntity", "VideoGame")
                        .WithMany("Platforms")
                        .HasForeignKey("VideoGameId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GameON.Web.Data.Entities.UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GameON.Web.Data.Entities.UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameON.Web.Data.Entities.UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GameON.Web.Data.Entities.UserEntity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
