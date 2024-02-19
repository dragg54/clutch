﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using clutch_position.Data.Contexts;

#nullable disable

namespace clutch_position.Migrations
{
    [DbContext(typeof(PositionDbContext))]
    partial class PositionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("clutch_position.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PositionDescription")
                        .HasColumnType("longtext")
                        .HasColumnName("positionDescription");

                    b.Property<string>("PositionName")
                        .HasColumnType("longtext")
                        .HasColumnName("positionName");

                    b.Property<int>("PositionStatus")
                        .HasColumnType("int")
                        .HasColumnName("positionStatus");

                    b.Property<int>("Salary")
                        .HasColumnType("int")
                        .HasColumnName("salary");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });
#pragma warning restore 612, 618
        }
    }
}
