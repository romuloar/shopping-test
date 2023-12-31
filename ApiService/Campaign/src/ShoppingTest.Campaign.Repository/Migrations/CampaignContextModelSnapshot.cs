﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingTest.Campaign.Repository.Context;

#nullable disable

namespace ShoppingTest.Campaign.Repository.Migrations
{
    [DbContext(typeof(CampaignContext))]
    partial class CampaignContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("ShoppingTest.Campaign.Core.Domain.CampaignDomain", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("IdShopping")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Campaign");
                });
#pragma warning restore 612, 618
        }
    }
}
