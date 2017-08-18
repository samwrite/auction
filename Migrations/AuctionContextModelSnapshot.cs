using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using auction.Models;

namespace auction.Migrations
{
    [DbContext(typeof(AuctionContext))]
    partial class AuctionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("auction.Models.Auction", b =>
                {
                    b.Property<int>("AuctionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("ItemId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("AuctionId");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Auctions");
                });

            modelBuilder.Entity("auction.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("CreatorUserId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<double>("HighestBid");

                    b.Property<int?>("HighestBidderUserId");

                    b.Property<string>("Name");

                    b.Property<double>("StartBid");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ItemId");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("HighestBidderUserId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("auction.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Balance");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("First");

                    b.Property<string>("Last");

                    b.Property<string>("Password");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("auction.Models.Auction", b =>
                {
                    b.HasOne("auction.Models.Item", "Item")
                        .WithMany("Auctions")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("auction.Models.User", "User")
                        .WithMany("Items")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("auction.Models.Item", b =>
                {
                    b.HasOne("auction.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");

                    b.HasOne("auction.Models.User", "HighestBidder")
                        .WithMany()
                        .HasForeignKey("HighestBidderUserId");
                });
        }
    }
}
