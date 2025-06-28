using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoEspectro.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UtilizadoresID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Artigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fotografia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artigos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodPostal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nif = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.ID);
                });

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

            migrationBuilder.CreateTable(
                name: "ArtigosUtilizadores",
                columns: table => new
                {
                    ListaArtigosId = table.Column<int>(type: "int", nullable: false),
                    ListaUtilizadosID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtigosUtilizadores", x => new { x.ListaArtigosId, x.ListaUtilizadosID });
                    table.ForeignKey(
                        name: "FK_ArtigosUtilizadores_Artigos_ListaArtigosId",
                        column: x => x.ListaArtigosId,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtigosUtilizadores_Utilizadores_ListaUtilizadosID",
                        column: x => x.ListaUtilizadosID,
                        principalTable: "Utilizadores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gostos",
                columns: table => new
                {
                    UtilizadorFk = table.Column<int>(type: "int", nullable: false),
                    ArtigoFK = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gostos", x => new { x.UtilizadorFk, x.ArtigoFK });
                    table.ForeignKey(
                        name: "FK_Gostos_Artigos_ArtigoFK",
                        column: x => x.ArtigoFK,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gostos_Utilizadores_UtilizadorFk",
                        column: x => x.UtilizadorFk,
                        principalTable: "Utilizadores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilizadorFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recursos_Utilizadores_UtilizadorFK",
                        column: x => x.UtilizadorFK,
                        principalTable: "Utilizadores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_AspNetUsers_UtilizadoresID",
                table: "AspNetUsers",
                column: "UtilizadoresID");

            migrationBuilder.CreateIndex(
                name: "IX_ArtigosCategorias_ListaCategoriasId",
                table: "ArtigosCategorias",
                column: "ListaCategoriasId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtigosRecursos_ListaRecursosId",
                table: "ArtigosRecursos",
                column: "ListaRecursosId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtigosUtilizadores_ListaUtilizadosID",
                table: "ArtigosUtilizadores",
                column: "ListaUtilizadosID");

            migrationBuilder.CreateIndex(
                name: "IX_Gostos_ArtigoFK",
                table: "Gostos",
                column: "ArtigoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_UtilizadorFK",
                table: "Recursos",
                column: "UtilizadorFK");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Utilizadores_UtilizadoresID",
                table: "AspNetUsers",
                column: "UtilizadoresID",
                principalTable: "Utilizadores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Utilizadores_UtilizadoresID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ArtigosCategorias");

            migrationBuilder.DropTable(
                name: "ArtigosRecursos");

            migrationBuilder.DropTable(
                name: "ArtigosUtilizadores");

            migrationBuilder.DropTable(
                name: "Gostos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Recursos");

            migrationBuilder.DropTable(
                name: "Artigos");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UtilizadoresID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UtilizadoresID",
                table: "AspNetUsers");
        }
    }
}
