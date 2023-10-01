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
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Solucao");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Feedback");

            migrationBuilder.AddColumn<string>(
                name: "DescSolucao",
                table: "Solucao",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeedbackText",
                table: "Feedback",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescSolucao",
                table: "Solucao");

            migrationBuilder.DropColumn(
                name: "FeedbackText",
                table: "Feedback");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Solucao",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Feedback",
                type: "TEXT",
                nullable: true);
        }
    }
}
