using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoEspectro.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirAplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UtilizadorID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UtilizadorID",
                table: "AspNetUsers",
                column: "UtilizadorID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Utilizadores_UtilizadorID",
                table: "AspNetUsers",
                column: "UtilizadorID",
                principalTable: "Utilizadores",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Utilizadores_UtilizadorID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UtilizadorID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UtilizadorID",
                table: "AspNetUsers");
        }
    }
}
