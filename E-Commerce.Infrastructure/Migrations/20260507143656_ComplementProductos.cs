using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ComplementProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_IdProducto_IdApplicationUser",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Productos_Precio_Nombre",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Productos");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdApplicationUser",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "Atributo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atributo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductoVariante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoVariante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductoVariante_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtributoValor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAtributo = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtributoValor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtributoValor_Atributo_IdAtributo",
                        column: x => x.IdAtributo,
                        principalTable: "Atributo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductoAtributoVariante",
                columns: table => new
                {
                    IdProductoVariante = table.Column<int>(type: "int", nullable: false),
                    IdAtributoValor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoAtributoVariante", x => new { x.IdProductoVariante, x.IdAtributoValor });
                    table.ForeignKey(
                        name: "FK_ProductoAtributoVariante_AtributoValor_IdAtributoValor",
                        column: x => x.IdAtributoValor,
                        principalTable: "AtributoValor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductoAtributoVariante_ProductoVariante_IdProductoVariante",
                        column: x => x.IdProductoVariante,
                        principalTable: "ProductoVariante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_IdProducto_IdApplicationUser",
                table: "Reviews",
                columns: new[] { "IdProducto", "IdApplicationUser" },
                unique: true,
                filter: "[IdApplicationUser] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Atributo_Nombre",
                table: "Atributo",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AtributoValor_IdAtributo_Valor",
                table: "AtributoValor",
                columns: new[] { "IdAtributo", "Valor" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductoAtributoVariante_IdAtributoValor",
                table: "ProductoAtributoVariante",
                column: "IdAtributoValor");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoVariante_IdProducto",
                table: "ProductoVariante",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoVariante_Precio_IdProducto",
                table: "ProductoVariante",
                columns: new[] { "Precio", "IdProducto" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoAtributoVariante");

            migrationBuilder.DropTable(
                name: "AtributoValor");

            migrationBuilder.DropTable(
                name: "ProductoVariante");

            migrationBuilder.DropTable(
                name: "Atributo");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_IdProducto_IdApplicationUser",
                table: "Reviews");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdApplicationUser",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Productos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_IdProducto_IdApplicationUser",
                table: "Reviews",
                columns: new[] { "IdProducto", "IdApplicationUser" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Precio_Nombre",
                table: "Productos",
                columns: new[] { "Precio", "Nombre" });
        }
    }
}
