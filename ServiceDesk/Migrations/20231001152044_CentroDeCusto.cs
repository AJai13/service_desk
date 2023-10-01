using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceDesk.Migrations
{
    /// <inheritdoc />
    public partial class CentroDeCusto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_CentroDeCustoId",
                table: "Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CentroDeCustoId",
                table: "Usuario",
                column: "CentroDeCustoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_CentroDeCustoId",
                table: "Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CentroDeCustoId",
                table: "Usuario",
                column: "CentroDeCustoId");
        }
    }
}
