﻿// <auto-generated />
using System;
using FIAB.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FIAB.Migrations
{
    [DbContext(typeof(FIABContext))]
    [Migration("20240224005123_InitialDBState")]
    partial class InitialDBState
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FIAB.Models.Noun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Nouns");
                });

            modelBuilder.Entity("FIAB.Models.Relationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("Ended")
                        .HasColumnType("date");

                    b.Property<bool>("EndedIsApproximate")
                        .HasColumnType("boolean");

                    b.Property<int>("ObjectId")
                        .HasColumnType("integer");

                    b.Property<int>("RelationshipTypeId")
                        .HasColumnType("integer");

                    b.Property<DateOnly?>("Started")
                        .HasColumnType("date");

                    b.Property<bool>("StartedIsApproximate")
                        .HasColumnType("boolean");

                    b.Property<int>("SubjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ObjectId");

                    b.HasIndex("RelationshipTypeId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Relationships");
                });

            modelBuilder.Entity("FIAB.Models.RelationshipType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("RelationshipTypes");
                });

            modelBuilder.Entity("FIAB.Models.Relationship", b =>
                {
                    b.HasOne("FIAB.Models.Noun", "Object")
                        .WithMany()
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FIAB.Models.RelationshipType", "RelationshipType")
                        .WithMany()
                        .HasForeignKey("RelationshipTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FIAB.Models.Noun", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Object");

                    b.Navigation("RelationshipType");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("FIAB.Models.RelationshipType", b =>
                {
                    b.HasOne("FIAB.Models.RelationshipType", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });
#pragma warning restore 612, 618
        }
    }
}
