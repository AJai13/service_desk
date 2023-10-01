using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceDesk.Migrations
{
    /// <inheritdoc />
    public partial class Feedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SolucaoId",
                table: "Feedback",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_SolucaoId",
                table: "Feedback",
                column: "SolucaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Solucao_SolucaoId",
                table: "Feedback",
                column: "SolucaoId",
                principalTable: "Solucao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Solucao_SolucaoId",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_SolucaoId",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "SolucaoId",
                table: "Feedback");
        }
    }
}
