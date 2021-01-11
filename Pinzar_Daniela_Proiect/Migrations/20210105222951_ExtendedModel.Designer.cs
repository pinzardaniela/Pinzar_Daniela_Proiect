﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pinzar_Daniela_Proiect.Data;

namespace Pinzar_Daniela_Proiect.Migrations
{
    [DbContext(typeof(MagazinContext))]
    [Migration("20210105222951_ExtendedModel")]
    partial class ExtendedModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pinzar_Daniela_Proiect.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNasterii")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nume")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientID");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Pinzar_Daniela_Proiect.Models.Comanda", b =>
                {
                    b.Property<int>("ComandaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataComenzii")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProdusID")
                        .HasColumnType("int");

                    b.HasKey("ComandaID");

                    b.HasIndex("ClientID");

                    b.HasIndex("ProdusID");

                    b.ToTable("Comanda");
                });

            modelBuilder.Entity("Pinzar_Daniela_Proiect.Models.Distribuitor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<string>("NumeDistribuitor")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Distribuitor");
                });

            modelBuilder.Entity("Pinzar_Daniela_Proiect.Models.DistribuitorProdus", b =>
                {
                    b.Property<int>("ProdusID")
                        .HasColumnType("int");

                    b.Property<int>("DistribuitorID")
                        .HasColumnType("int");

                    b.HasKey("ProdusID", "DistribuitorID");

                    b.HasIndex("DistribuitorID");

                    b.ToTable("DistribuitorProdus");
                });

            modelBuilder.Entity("Pinzar_Daniela_Proiect.Models.Produs", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Denumire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Furnizor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Pret")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("Produs");
                });

            modelBuilder.Entity("Pinzar_Daniela_Proiect.Models.Comanda", b =>
                {
                    b.HasOne("Pinzar_Daniela_Proiect.Models.Client", "Client")
                        .WithMany("Comenzi")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pinzar_Daniela_Proiect.Models.Produs", "Produs")
                        .WithMany("Comenzi")
                        .HasForeignKey("ProdusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pinzar_Daniela_Proiect.Models.DistribuitorProdus", b =>
                {
                    b.HasOne("Pinzar_Daniela_Proiect.Models.Distribuitor", "Distribuitor")
                        .WithMany("DistribuitorProduse")
                        .HasForeignKey("DistribuitorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pinzar_Daniela_Proiect.Models.Produs", "Produs")
                        .WithMany("DistribuitorProduse")
                        .HasForeignKey("ProdusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
