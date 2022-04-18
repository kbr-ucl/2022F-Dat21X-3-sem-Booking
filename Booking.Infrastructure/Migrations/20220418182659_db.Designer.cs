﻿// <auto-generated />
using System;
using Booking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Booking.Infrastructure.Migrations
{
    [DbContext(typeof(BookingContext))]
    [Migration("20220418182659_db")]
    partial class db
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Booking.Domain.Entities.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BookingId");

                    b.Property<DateTime>("Slut")
                        .HasColumnType("datetime2")
                        .HasColumnName("End");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2")
                        .HasColumnName("Begin");

                    b.HasKey("Id");

                    b.ToTable("Booking", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
