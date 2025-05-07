using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoEspectro.Data.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacao_Professor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artigos_Utilizadores_UtilizadorFK",
                table: "Artigos");

            migrationBuilder.DropForeignKey(
                name: "FK_Recursos_Utilizadores_UtilizadorFK",
                table: "Recursos");

            migrationBuilder.DropTable(
                name: "ArtigosRecursos");

            migrationBuilder.RenameColumn(
                name: "Nif",
                table: "Utilizadores",
                newName: "NIF");

            migrationBuilder.RenameColumn(
                name: "UtilizadorFK",
                table: "Recursos",
                newName: "AutorFK");

            migrationBuilder.RenameIndex(
                name: "IX_Recursos_UtilizadorFK",
                table: "Recursos",
                newName: "IX_Recursos_AutorFK");

            migrationBuilder.RenameColumn(
                name: "UtilizadorFK",
                table: "Artigos",
                newName: "AutorFK");

            migrationBuilder.RenameIndex(
                name: "IX_Artigos_UtilizadorFK",
                table: "Artigos",
                newName: "IX_Artigos_AutorFK");

            migrationBuilder.AddColumn<string>(
                name: "Ficheiro",
                table: "Recursos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Detalhes",
                columns: table => new
                {
                    RecursoFK = table.Column<int>(type: "int", nullable: false),
                    ArtigoFK = table.Column<int>(type: "int", nullable: false),
                    Principal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalhes", x => new { x.RecursoFK, x.ArtigoFK });
                    table.ForeignKey(
                        name: "FK_Detalhes_Artigos_ArtigoFK",
                        column: x => x.ArtigoFK,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detalhes_Recursos_RecursoFK",
                        column: x => x.RecursoFK,
                        principalTable: "Recursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalhes_ArtigoFK",
                table: "Detalhes",
                column: "ArtigoFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Artigos_Utilizadores_AutorFK",
                table: "Artigos",
                column: "AutorFK",
                principalTable: "Utilizadores",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Recursos_Utilizadores_AutorFK",
                table: "Recursos",
                column: "AutorFK",
                principalTable: "Utilizadores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artigos_Utilizadores_AutorFK",
                table: "Artigos");

            migrationBuilder.DropForeignKey(
                name: "FK_Recursos_Utilizadores_AutorFK",
                table: "Recursos");

            migrationBuilder.DropTable(
                name: "Detalhes");

            migrationBuilder.DropColumn(
                name: "Ficheiro",
                table: "Recursos");

            migrationBuilder.RenameColumn(
                name: "NIF",
                table: "Utilizadores",
                newName: "Nif");

            migrationBuilder.RenameColumn(
                name: "AutorFK",
                table: "Recursos",
                newName: "UtilizadorFK");

            migrationBuilder.RenameIndex(
                name: "IX_Recursos_AutorFK",
                table: "Recursos",
                newName: "IX_Recursos_UtilizadorFK");

            migrationBuilder.RenameColumn(
                name: "AutorFK",
                table: "Artigos",
                newName: "UtilizadorFK");

            migrationBuilder.RenameIndex(
                name: "IX_Artigos_AutorFK",
                table: "Artigos",
                newName: "IX_Artigos_UtilizadorFK");

            migrationBuilder.CreateTable(
                name: "ArtigosRecursos",
                columns: table => new
                {
                    ListaArtigosId = table.Column<int>(type: "int", nullable: false),
                    ListaRecursosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtigosRecursos", x => new { x.ListaArtigosId, x.ListaRecursosId });
                    table.ForeignKey(
                        name: "FK_ArtigosRecursos_Artigos_ListaArtigosId",
                        column: x => x.ListaArtigosId,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtigosRecursos_Recursos_ListaRecursosId",
                        column: x => x.ListaRecursosId,
                        principalTable: "Recursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtigosRecursos_ListaRecursosId",
                table: "ArtigosRecursos",
                column: "ListaRecursosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artigos_Utilizadores_UtilizadorFK",
                table: "Artigos",
                column: "UtilizadorFK",
                principalTable: "Utilizadores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recursos_Utilizadores_UtilizadorFK",
                table: "Recursos",
                column: "UtilizadorFK",
                principalTable: "Utilizadores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
