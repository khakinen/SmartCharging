﻿// <auto-generated />
using System;
using Data.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Implementation.Migrations
{
    [DbContext(typeof(SmartChargingDbContext))]
    partial class SmartChargingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SmartCharging.Data.Contract.Models.ChargeStation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("ChargeStations");
                });

            modelBuilder.Entity("SmartCharging.Data.Contract.Models.Connector", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChargeStationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ConnectorNumber")
                        .HasColumnType("int");

                    b.Property<int>("MaxCurrentInAmps")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChargeStationId");

                    b.ToTable("Connectors");
                });

            modelBuilder.Entity("SmartCharging.Data.Contract.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CapacityInAmps")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SmartCharging.Data.Contract.Models.ChargeStation", b =>
                {
                    b.HasOne("SmartCharging.Data.Contract.Models.Group", "Group")
                        .WithMany("ChargeStations")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("SmartCharging.Data.Contract.Models.Connector", b =>
                {
                    b.HasOne("SmartCharging.Data.Contract.Models.ChargeStation", "ChargeStation")
                        .WithMany("Connectors")
                        .HasForeignKey("ChargeStationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChargeStation");
                });

            modelBuilder.Entity("SmartCharging.Data.Contract.Models.ChargeStation", b =>
                {
                    b.Navigation("Connectors");
                });

            modelBuilder.Entity("SmartCharging.Data.Contract.Models.Group", b =>
                {
                    b.Navigation("ChargeStations");
                });
#pragma warning restore 612, 618
        }
    }
}
