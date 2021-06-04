﻿// <auto-generated />
using System;
using Funpoly.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Funpoly.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210604160324_postcardTeam-table")]
    partial class postcardTeamtable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Funpoly.Data.Models.BoardSquare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("BoardSquares");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Continent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Continents");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Parcel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BoardSquareId")
                        .HasColumnType("integer");

                    b.Property<int>("ContinentId")
                        .HasColumnType("integer");

                    b.Property<int>("HotelPrice")
                        .HasColumnType("integer");

                    b.Property<int>("HotelTax")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("RawTax")
                        .HasColumnType("integer");

                    b.Property<int>("TwoHotelsTax")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BoardSquareId")
                        .IsUnique();

                    b.HasIndex("ContinentId");

                    b.ToTable("Parcels");
                });

            modelBuilder.Entity("Funpoly.Data.Models.ParcelProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("HotelBuilt")
                        .HasColumnType("boolean");

                    b.Property<int>("ParcelId")
                        .HasColumnType("integer");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParcelId");

                    b.HasIndex("TeamId");

                    b.ToTable("ParcelProperty");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Captain")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Postcard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ParcelId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParcelId")
                        .IsUnique();

                    b.ToTable("Postcards");
                });

            modelBuilder.Entity("Funpoly.Data.Models.PostcardTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("PostcardId")
                        .HasColumnType("integer");

                    b.Property<int>("TeamId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PostcardId");

                    b.HasIndex("TeamId");

                    b.ToTable("PostcardTeam");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BoardSquareId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Cash")
                        .HasColumnType("numeric");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ConsecutiveSixes")
                        .HasColumnType("integer");

                    b.Property<int>("Days")
                        .HasColumnType("integer");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<bool>("InPrison")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TransportId")
                        .HasColumnType("integer");

                    b.Property<int>("Turn")
                        .HasColumnType("integer");

                    b.Property<int>("TurnsInPrison")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BoardSquareId");

                    b.HasIndex("GameId");

                    b.HasIndex("TransportId")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Transport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Dices")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Substraction")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Parcel", b =>
                {
                    b.HasOne("Funpoly.Data.Models.BoardSquare", "BoardSquare")
                        .WithOne("Parcel")
                        .HasForeignKey("Funpoly.Data.Models.Parcel", "BoardSquareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Funpoly.Data.Models.Continent", "Continent")
                        .WithMany("Parcels")
                        .HasForeignKey("ContinentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoardSquare");

                    b.Navigation("Continent");
                });

            modelBuilder.Entity("Funpoly.Data.Models.ParcelProperty", b =>
                {
                    b.HasOne("Funpoly.Data.Models.Parcel", "Parcel")
                        .WithMany("ParcelProperties")
                        .HasForeignKey("ParcelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Funpoly.Data.Models.Team", "Team")
                        .WithMany("ParcelProperties")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parcel");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Player", b =>
                {
                    b.HasOne("Funpoly.Data.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Postcard", b =>
                {
                    b.HasOne("Funpoly.Data.Models.Parcel", "Parcel")
                        .WithOne("Postcard")
                        .HasForeignKey("Funpoly.Data.Models.Postcard", "ParcelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parcel");
                });

            modelBuilder.Entity("Funpoly.Data.Models.PostcardTeam", b =>
                {
                    b.HasOne("Funpoly.Data.Models.Postcard", "Postcard")
                        .WithMany("PostcardTeams")
                        .HasForeignKey("PostcardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Funpoly.Data.Models.Team", "Team")
                        .WithMany("PostcardTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Postcard");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Team", b =>
                {
                    b.HasOne("Funpoly.Data.Models.BoardSquare", "BoardSquare")
                        .WithMany("Teams")
                        .HasForeignKey("BoardSquareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Funpoly.Data.Models.Game", "Game")
                        .WithMany("Teams")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Funpoly.Data.Models.Transport", "Transport")
                        .WithOne("Team")
                        .HasForeignKey("Funpoly.Data.Models.Team", "TransportId");

                    b.Navigation("BoardSquare");

                    b.Navigation("Game");

                    b.Navigation("Transport");
                });

            modelBuilder.Entity("Funpoly.Data.Models.BoardSquare", b =>
                {
                    b.Navigation("Parcel");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Continent", b =>
                {
                    b.Navigation("Parcels");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Game", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Parcel", b =>
                {
                    b.Navigation("ParcelProperties");

                    b.Navigation("Postcard");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Postcard", b =>
                {
                    b.Navigation("PostcardTeams");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Team", b =>
                {
                    b.Navigation("ParcelProperties");

                    b.Navigation("Players");

                    b.Navigation("PostcardTeams");
                });

            modelBuilder.Entity("Funpoly.Data.Models.Transport", b =>
                {
                    b.Navigation("Team");
                });
#pragma warning restore 612, 618
        }
    }
}
