﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Records.DAL;

#nullable disable

namespace Records.DAL.Migrations
{
    [DbContext(typeof(RecordsContext))]
    partial class RecordsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.16");

            modelBuilder.Entity("Records.Data.Models.BestRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("GreenScore")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PinkScore")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalScore")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("BestRecords");
                });

            modelBuilder.Entity("Records.Data.Models.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BestRecordId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BestRecordId")
                        .IsUnique();

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Records.Data.Models.Player", b =>
                {
                    b.HasOne("Records.Data.Models.BestRecord", "BestRecord")
                        .WithOne("Player")
                        .HasForeignKey("Records.Data.Models.Player", "BestRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BestRecord");
                });

            modelBuilder.Entity("Records.Data.Models.BestRecord", b =>
                {
                    b.Navigation("Player")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}