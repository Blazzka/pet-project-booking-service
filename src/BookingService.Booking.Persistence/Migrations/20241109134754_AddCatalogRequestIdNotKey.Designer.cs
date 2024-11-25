﻿// <auto-generated />
using System;
using BookingService.Booking.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookingService.Booking.Persistence.Migrations
{
    [DbContext(typeof(BookingsContext))]
    [Migration("20241109134754_AddCatalogRequestIdNotKey")]
    partial class AddCatalogRequestIdNotKey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookingService.Booking.Domain.Bookings.BookingAggregate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateOnly>("BookedFrom")
                        .HasColumnType("date")
                        .HasColumnName("booked_from");

                    b.Property<DateOnly>("BookedTo")
                        .HasColumnType("date")
                        .HasColumnName("booked_to");

                    b.Property<Guid?>("CatalogRequestId")
                        .HasColumnType("uuid")
                        .HasColumnName("catalog_request_id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<long>("ResourceId")
                        .HasColumnType("bigint")
                        .HasColumnName("resource_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_bookings");

                    b.ToTable("Bookings", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
