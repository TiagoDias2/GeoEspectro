using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoEspectro.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirNomeUtilizadores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Utilizadores_UtilizadorID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UtilizadorID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UtilizadorID",
                table: "AspNetUsers",
                newName: "UtilizadoresID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UtilizadoresID",
                table: "AspNetUsers",
                column: "UtilizadoresID",
                unique: true,
                filter: "[UtilizadoresID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Utilizadores_UtilizadoresID",
                table: "AspNetUsers",
                column: "UtilizadoresID",
                principalTable: "Utilizadores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Utilizadores_UtilizadoresID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UtilizadoresID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UtilizadoresID",
                table: "AspNetUsers",
                newName: "UtilizadorID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UtilizadorID",
                table: "AspNetUsers",
                column: "UtilizadorID",
                unique: true,
                filter: "[UtilizadorID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Utilizadores_UtilizadorID",
                table: "AspNetUsers",
                column: "UtilizadorID",
                principalTable: "Utilizadores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
