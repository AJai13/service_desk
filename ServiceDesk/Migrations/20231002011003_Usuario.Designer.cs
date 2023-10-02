﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceDesk.Data;

#nullable disable

namespace ServiceDesk.Migrations
{
    [DbContext(typeof(ServiceDeskDbContext))]
    [Migration("20231002011003_Usuario")]
    partial class Usuario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FeedbackText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SolucaoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SolucaoId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cargo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("ServiceDesk.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("ServiceDesk.CentroDeCusto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CentroDeCusto");
                });

            modelBuilder.Entity("ServiceDesk.Dispositivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Dispositivo");
                });

            modelBuilder.Entity("ServiceDesk.Filtro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Filtro");
                });

            modelBuilder.Entity("ServiceDesk.Models.Sla", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sla");
                });

            modelBuilder.Entity("ServiceDesk.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DispositivoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Titulo")
                        .HasColumnType("TEXT");

                    b.Property<int?>("categoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("dataAbertura")
                        .HasColumnType("TEXT");

                    b.Property<int?>("funcionarioResponsavelId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("propriedadeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("slaId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("statusId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("usuarioCriadorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DispositivoId");

                    b.HasIndex("categoriaId");

                    b.HasIndex("funcionarioResponsavelId");

                    b.HasIndex("propriedadeId");

                    b.HasIndex("slaId");

                    b.HasIndex("statusId");

                    b.HasIndex("usuarioCriadorId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("ServiceDesk.Prioridade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Prioridade");
                });

            modelBuilder.Entity("ServiceDesk.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("ServiceDesk.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CentroDeCustoId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DispositivoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CentroDeCustoId")
                        .IsUnique();

                    b.HasIndex("DispositivoId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Solucao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescSolucao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Solucao");
                });

            modelBuilder.Entity("Feedback", b =>
                {
                    b.HasOne("Solucao", "Solucao")
                        .WithMany()
                        .HasForeignKey("SolucaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Solucao");
                });

            modelBuilder.Entity("ServiceDesk.Models.Ticket", b =>
                {
                    b.HasOne("ServiceDesk.Dispositivo", "Dispositivo")
                        .WithMany()
                        .HasForeignKey("DispositivoId");

                    b.HasOne("ServiceDesk.Categoria", "categoria")
                        .WithMany()
                        .HasForeignKey("categoriaId");

                    b.HasOne("Funcionario", "funcionarioResponsavel")
                        .WithMany("Ticket")
                        .HasForeignKey("funcionarioResponsavelId");

                    b.HasOne("ServiceDesk.Prioridade", "propriedade")
                        .WithMany()
                        .HasForeignKey("propriedadeId");

                    b.HasOne("ServiceDesk.Models.Sla", "sla")
                        .WithMany("Tickets")
                        .HasForeignKey("slaId");

                    b.HasOne("ServiceDesk.Status", "status")
                        .WithMany()
                        .HasForeignKey("statusId");

                    b.HasOne("ServiceDesk.Usuario", "usuarioCriador")
                        .WithMany()
                        .HasForeignKey("usuarioCriadorId");

                    b.Navigation("Dispositivo");

                    b.Navigation("categoria");

                    b.Navigation("funcionarioResponsavel");

                    b.Navigation("propriedade");

                    b.Navigation("sla");

                    b.Navigation("status");

                    b.Navigation("usuarioCriador");
                });

            modelBuilder.Entity("ServiceDesk.Usuario", b =>
                {
                    b.HasOne("ServiceDesk.CentroDeCusto", "CentroDeCusto")
                        .WithOne("Usuario")
                        .HasForeignKey("ServiceDesk.Usuario", "CentroDeCustoId");

                    b.HasOne("ServiceDesk.Dispositivo", "Dispositivo")
                        .WithMany()
                        .HasForeignKey("DispositivoId");

                    b.Navigation("CentroDeCusto");

                    b.Navigation("Dispositivo");
                });

            modelBuilder.Entity("Funcionario", b =>
                {
                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("ServiceDesk.CentroDeCusto", b =>
                {
                    b.Navigation("Usuario")
                        .IsRequired();
                });

            modelBuilder.Entity("ServiceDesk.Models.Sla", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
