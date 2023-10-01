using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceDesk.Migrations
{
    /// <inheritdoc />
    public partial class CreatePrioridadeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Prioridades_propriedadeId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prioridades",
                table: "Prioridades");

            migrationBuilder.RenameTable(
                name: "Prioridades",
                newName: "Prioridade");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prioridade",
                table: "Prioridade",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Prioridade_propriedadeId",
                table: "Ticket",
                column: "propriedadeId",
                principalTable: "Prioridade",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Prioridade_propriedadeId",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prioridade",
                table: "Prioridade");

            migrationBuilder.RenameTable(
                name: "Prioridade",
                newName: "Prioridades");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prioridades",
                table: "Prioridades",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Prioridades_propriedadeId",
                table: "Ticket",
                column: "propriedadeId",
                principalTable: "Prioridades",
                principalColumn: "Id");
        }
    }
}
