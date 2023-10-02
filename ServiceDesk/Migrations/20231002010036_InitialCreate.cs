using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceDesk.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CentroDeCusto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroDeCusto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dispositivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispositivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filtro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filtro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Senha = table.Column<string>(type: "TEXT", nullable: true),
                    Cargo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prioridade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prioridade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sla",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sla", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solucao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DescSolucao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solucao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    DispositivoId = table.Column<int>(type: "INTEGER", nullable: true),
                    CentroDeCustoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_CentroDeCusto_CentroDeCustoId",
                        column: x => x.CentroDeCustoId,
                        principalTable: "CentroDeCusto",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_Dispositivo_DispositivoId",
                        column: x => x.DispositivoId,
                        principalTable: "Dispositivo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FeedbackText = table.Column<string>(type: "TEXT", nullable: false),
                    SolucaoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Solucao_SolucaoId",
                        column: x => x.SolucaoId,
                        principalTable: "Solucao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    dataAbertura = table.Column<DateTime>(type: "TEXT", nullable: false),
                    statusId = table.Column<int>(type: "INTEGER", nullable: true),
                    propriedadeId = table.Column<int>(type: "INTEGER", nullable: true),
                    categoriaId = table.Column<int>(type: "INTEGER", nullable: true),
                    slaId = table.Column<int>(type: "INTEGER", nullable: true),
                    DispositivoId = table.Column<int>(type: "INTEGER", nullable: true),
                    usuarioCriadorId = table.Column<int>(type: "INTEGER", nullable: true),
                    funcionarioResponsavelId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Categoria_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Dispositivo_DispositivoId",
                        column: x => x.DispositivoId,
                        principalTable: "Dispositivo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Funcionario_funcionarioResponsavelId",
                        column: x => x.funcionarioResponsavelId,
                        principalTable: "Funcionario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Prioridade_propriedadeId",
                        column: x => x.propriedadeId,
                        principalTable: "Prioridade",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Sla_slaId",
                        column: x => x.slaId,
                        principalTable: "Sla",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Status_statusId",
                        column: x => x.statusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Usuario_usuarioCriadorId",
                        column: x => x.usuarioCriadorId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_SolucaoId",
                table: "Feedback",
                column: "SolucaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_categoriaId",
                table: "Ticket",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_DispositivoId",
                table: "Ticket",
                column: "DispositivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_funcionarioResponsavelId",
                table: "Ticket",
                column: "funcionarioResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_propriedadeId",
                table: "Ticket",
                column: "propriedadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_slaId",
                table: "Ticket",
                column: "slaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_statusId",
                table: "Ticket",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_usuarioCriadorId",
                table: "Ticket",
                column: "usuarioCriadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CentroDeCustoId",
                table: "Usuario",
                column: "CentroDeCustoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_DispositivoId",
                table: "Usuario",
                column: "DispositivoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Filtro");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Solucao");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Prioridade");

            migrationBuilder.DropTable(
                name: "Sla");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "CentroDeCusto");

            migrationBuilder.DropTable(
                name: "Dispositivo");
        }
    }
}
