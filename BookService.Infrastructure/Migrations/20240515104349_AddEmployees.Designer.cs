﻿// <auto-generated />
using System;
using System.Collections.Generic;
using BookService.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookService.Infrastructure.Migrations
{
    [DbContext(typeof(BookServiceDbContext))]
    [Migration("20240515104349_AddEmployees")]
    partial class AddEmployees
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookService.Domain.Models.AuthorizationToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpiresAtUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AuthorizationTokens");
                });

            modelBuilder.Entity("BookService.Domain.Models.Client", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BookService.Domain.Models.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("RestaurantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BookService.Domain.Models.Reservation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PersonsCount")
                        .HasColumnType("integer");

                    b.Property<long>("RestaurantId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("BookService.Domain.Models.Restaurant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double>("Cost")
                        .HasColumnType("double precision");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EndWorkTimeLocal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("KitchenType")
                        .HasColumnType("integer");

                    b.Property<int>("ReservationThreshold")
                        .HasColumnType("integer");

                    b.Property<string>("StartWorkTimeLocal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("BookService.Domain.Models.Table", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("ReserveAll")
                        .HasColumnType("boolean");

                    b.Property<long>("RestaurantId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("BookService.Domain.Models.Reservation", b =>
                {
                    b.OwnsOne("BookService.Domain.Models.TableInfo", "Table", b1 =>
                        {
                            b1.Property<long>("ReservationId")
                                .HasColumnType("bigint");

                            b1.Property<List<long>>("PlaceIds")
                                .IsRequired()
                                .HasColumnType("bigint[]");

                            b1.Property<long>("TableId")
                                .HasColumnType("bigint");

                            b1.HasKey("ReservationId");

                            b1.ToTable("Reservations");

                            b1.ToJson("Table");

                            b1.WithOwner()
                                .HasForeignKey("ReservationId");
                        });

                    b.Navigation("Table")
                        .IsRequired();
                });

            modelBuilder.Entity("BookService.Domain.Models.Restaurant", b =>
                {
                    b.OwnsOne("BookService.Domain.Models.RestaurantContact", "Contact", b1 =>
                        {
                            b1.Property<long>("RestaurantId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("RestaurantId");

                            b1.ToTable("Restaurants");

                            b1.ToJson("Contact");

                            b1.WithOwner()
                                .HasForeignKey("RestaurantId");
                        });

                    b.OwnsMany("BookService.Domain.Models.RestaurantMenuItem", "MenuItems", b1 =>
                        {
                            b1.Property<long>("RestaurantId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<double>("Cost")
                                .HasColumnType("double precision");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Weight")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("RestaurantId", "Id");

                            b1.ToTable("Restaurants");

                            b1.ToJson("MenuItems");

                            b1.WithOwner()
                                .HasForeignKey("RestaurantId");
                        });

                    b.OwnsMany("BookService.Domain.Models.RestaurantPicture", "Pictures", b1 =>
                        {
                            b1.Property<long>("RestaurantId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("RestaurantId", "Id");

                            b1.ToTable("Restaurants");

                            b1.ToJson("Pictures");

                            b1.WithOwner()
                                .HasForeignKey("RestaurantId");
                        });

                    b.Navigation("Contact")
                        .IsRequired();

                    b.Navigation("MenuItems");

                    b.Navigation("Pictures");
                });

            modelBuilder.Entity("BookService.Domain.Models.Table", b =>
                {
                    b.OwnsMany("BookService.Domain.Models.TablePlace", "Places", b1 =>
                        {
                            b1.Property<long>("TableId")
                                .HasColumnType("bigint");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<long>("Number")
                                .HasColumnType("bigint");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("TableId", "Id");

                            b1.ToTable("TablePlace");

                            b1.WithOwner()
                                .HasForeignKey("TableId");
                        });

                    b.Navigation("Places");
                });
#pragma warning restore 612, 618
        }
    }
}
