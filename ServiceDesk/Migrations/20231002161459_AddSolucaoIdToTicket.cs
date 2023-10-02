using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceDesk.Migrations
{
    /// <inheritdoc />
    public partial class AddSolucaoIdToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SolucaoId",
                table: "Ticket",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SolucaoId",
                table: "Ticket",
                column: "SolucaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Solucao_SolucaoId",
                table: "Ticket",
                column: "SolucaoId",
                principalTable: "Solucao",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Solucao_SolucaoId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_SolucaoId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "SolucaoId",
                table: "Ticket");
        }
    }
}
