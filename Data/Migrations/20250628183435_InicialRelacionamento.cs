using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoEspectro.Data.Migrations
{
    /// <inheritdoc />
    public partial class InicialRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        {
            migrationBuilder.AddColumn<int>(
                name: "UtilizadoresID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilizadoresID1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

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
                    Morada = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodPostal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Pais = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Nif = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Telemovel = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Artigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutorFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artigos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artigos_Utilizadores_AutorFK",
                        column: x => x.AutorFK,
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
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ficheiro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutorFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recursos_Utilizadores_AutorFK",
                        column: x => x.AutorFK,
                        principalTable: "Utilizadores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_AspNetUsers_UtilizadoresID",
                table: "AspNetUsers",
                column: "UtilizadoresID",
                unique: true,
                filter: "[UtilizadoresID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UtilizadoresID1",
                table: "AspNetUsers",
                column: "UtilizadoresID1",
                unique: true,
                filter: "[UtilizadoresID1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_AutorFK",
                table: "Artigos",
                column: "AutorFK");

            migrationBuilder.CreateIndex(
                name: "IX_ArtigosCategoria_CategoriaId",
                table: "ArtigosCategoria",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Detalhes_ArtigoFK",
                table: "Detalhes",
                column: "ArtigoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Gostos_ArtigoFK",
                table: "Gostos",
                column: "ArtigoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_AutorFK",
                table: "Recursos",
                column: "AutorFK");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Utilizadores_UtilizadoresID",
                table: "AspNetUsers",
                column: "UtilizadoresID",
                principalTable: "Utilizadores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Utilizadores_UtilizadoresID1",
                table: "AspNetUsers",
                column: "UtilizadoresID1",
                principalTable: "Utilizadores",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Utilizadores_UtilizadoresID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Utilizadores_UtilizadoresID1",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ArtigosCategoria");

            migrationBuilder.DropTable(
                name: "Detalhes");

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

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UtilizadoresID1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UtilizadoresID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UtilizadoresID1",
                table: "AspNetUsers");
        }
    }
}
