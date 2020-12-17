﻿// <auto-generated />
using System;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookShop.Migrations
{
    [DbContext(typeof(LibrosContext))]
    [Migration("20201216005000_ModificacionCliente")]
    partial class ModificacionCliente
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("BookShop.Models.Autor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("nombreCompleto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("BookShop.Models.Carrito", b =>
                {
                    b.Property<string>("ClienteID")
                        .HasColumnType("TEXT");

                    b.HasKey("ClienteID");

                    b.ToTable("Carritos");
                });

            modelBuilder.Entity("BookShop.Models.Cliente", b =>
                {
                    b.Property<string>("Mail")
                        .HasColumnType("TEXT");

                    b.Property<string>("CarritoClienteID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Mail");

                    b.HasIndex("CarritoClienteID");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("BookShop.Models.Genero", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescripcionGenero")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("BookShop.Models.Libro", b =>
                {
                    b.Property<string>("ISBN")
                        .HasColumnType("TEXT");

                    b.Property<string>("CarritoClienteID")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ClasificacionID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreadorID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescripcionLibro")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Portada")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Precio")
                        .HasColumnType("REAL");

                    b.Property<int>("Stock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ISBN");

                    b.HasIndex("CarritoClienteID");

                    b.HasIndex("ClasificacionID");

                    b.HasIndex("CreadorID");

                    b.ToTable("Libros");
                });

            modelBuilder.Entity("BookShop.Models.Cliente", b =>
                {
                    b.HasOne("BookShop.Models.Carrito", "Carrito")
                        .WithMany()
                        .HasForeignKey("CarritoClienteID");

                    b.Navigation("Carrito");
                });

            modelBuilder.Entity("BookShop.Models.Libro", b =>
                {
                    b.HasOne("BookShop.Models.Carrito", null)
                        .WithMany("Libros")
                        .HasForeignKey("CarritoClienteID");

                    b.HasOne("BookShop.Models.Genero", "Clasificacion")
                        .WithMany()
                        .HasForeignKey("ClasificacionID");

                    b.HasOne("BookShop.Models.Autor", "Creador")
                        .WithMany("Libros")
                        .HasForeignKey("CreadorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clasificacion");

                    b.Navigation("Creador");
                });

            modelBuilder.Entity("BookShop.Models.Autor", b =>
                {
                    b.Navigation("Libros");
                });

            modelBuilder.Entity("BookShop.Models.Carrito", b =>
                {
                    b.Navigation("Libros");
                });
#pragma warning restore 612, 618
        }
    }
}