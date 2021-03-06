﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Persistence.Migrations
{
    [DbContext(typeof(StarWarsSampleAppDbContext))]
    partial class StarWarsSampleAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StarWarsSampleApp.Domain.Entities.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("StarWarsSampleApp.Domain.Entities.CharacterEpisode", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<int>("EpisodeId");

                    b.HasKey("CharacterId", "EpisodeId");

                    b.HasIndex("EpisodeId");

                    b.ToTable("CharacterEpisodes");
                });

            modelBuilder.Entity("StarWarsSampleApp.Domain.Entities.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("StarWarsSampleApp.Domain.Entities.Friendship", b =>
                {
                    b.Property<int>("CharacterId");

                    b.Property<int>("FriendId");

                    b.HasKey("CharacterId", "FriendId");

                    b.HasIndex("FriendId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("StarWarsSampleApp.Domain.Entities.CharacterEpisode", b =>
                {
                    b.HasOne("StarWarsSampleApp.Domain.Entities.Character", "Character")
                        .WithMany("Episodes")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StarWarsSampleApp.Domain.Entities.Episode", "Episode")
                        .WithMany("Characters")
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StarWarsSampleApp.Domain.Entities.Friendship", b =>
                {
                    b.HasOne("StarWarsSampleApp.Domain.Entities.Character", "Character")
                        .WithMany("Friends")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StarWarsSampleApp.Domain.Entities.Character", "Friend")
                        .WithMany()
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
