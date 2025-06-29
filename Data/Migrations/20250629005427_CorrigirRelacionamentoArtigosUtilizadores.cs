using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoEspectro.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirRelacionamentoArtigosUtilizadores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recursos_Utilizadores_UtilizadorFK",
                table: "Recursos");

            migrationBuilder.DropTable(
                name: "ArtigosCategorias");

            migrationBuilder.DropTable(
                name: "ArtigosRecursos");

            migrationBuilder.DropTable(
                name: "ArtigosUtilizadores");

            migrationBuilder.DropColumn(
                name: "Fotografia",
                table: "Artigos");

            migrationBuilder.RenameColumn(
                name: "UtilizadorFK",
                table: "Recursos",
                newName: "AutorFK");

            migrationBuilder.RenameIndex(
                name: "IX_Recursos_UtilizadorFK",
                table: "Recursos",
                newName: "IX_Recursos_AutorFK");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Utilizadores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Telemovel",
                table: "Utilizadores",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(18)",
                oldMaxLength: 18);

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Utilizadores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nif",
                table: "Utilizadores",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<string>(
                name: "Morada",
                table: "Utilizadores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CodPostal",
                table: "Utilizadores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Recursos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "Recursos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Recursos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Local",
                table: "Recursos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Ficheiro",
                table: "Recursos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Artigos",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Texto",
                table: "Artigos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AutorFK",
                table: "Artigos",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Artigos_Utilizadores_AutorFK",
                table: "Artigos",
                column: "AutorFK",
                principalTable: "Utilizadores",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
                name: "ArtigosCategoria");

            migrationBuilder.DropTable(
                name: "Detalhes");

            migrationBuilder.DropIndex(
                name: "IX_Artigos_AutorFK",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "Ficheiro",
                table: "Recursos");

            migrationBuilder.DropColumn(
                name: "AutorFK",
                table: "Artigos");

            migrationBuilder.RenameColumn(
                name: "AutorFK",
                table: "Recursos",
                newName: "UtilizadorFK");

            migrationBuilder.RenameIndex(
                name: "IX_Recursos_AutorFK",
                table: "Recursos",
                newName: "IX_Recursos_UtilizadorFK");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Utilizadores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telemovel",
                table: "Utilizadores",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(18)",
                oldMaxLength: 18,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Utilizadores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nif",
                table: "Utilizadores",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Morada",
                table: "Utilizadores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodPostal",
                table: "Utilizadores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Recursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "Recursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Recursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Local",
                table: "Recursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Artigos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "Texto",
                table: "Artigos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fotografia",
                table: "Artigos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
