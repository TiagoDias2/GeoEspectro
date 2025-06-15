using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoEspectro.Data.Migrations
{
    /// <inheritdoc />
    public partial class InsercaoClasseIntermedia_Categoria_Artigo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtigosCategorias");

            migrationBuilder.CreateTable(
                name: "ArtigosCategoria",
                columns: table => new
                {
                    ArtigosId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtigosCategoria", x => new { x.ArtigosId, x.CategoriaId });
                    table.ForeignKey(
                        name: "FK_ArtigosCategoria_Artigos_ArtigosId",
                        column: x => x.ArtigosId,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtigosCategoria_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtigosCategoria_CategoriaId",
                table: "ArtigosCategoria",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtigosCategoria");

            migrationBuilder.CreateTable(
                name: "ArtigosCategorias",
                columns: table => new
                {
                    ListaArtigosId = table.Column<int>(type: "int", nullable: false),
                    ListaCategoriasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtigosCategorias", x => new { x.ListaArtigosId, x.ListaCategoriasId });
                    table.ForeignKey(
                        name: "FK_ArtigosCategorias_Artigos_ListaArtigosId",
                        column: x => x.ListaArtigosId,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtigosCategorias_Categorias_ListaCategoriasId",
                        column: x => x.ListaCategoriasId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtigosCategorias_ListaCategoriasId",
                table: "ArtigosCategorias",
                column: "ListaCategoriasId");
        }
    }
}
