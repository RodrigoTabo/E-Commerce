using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ComplementProductos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AtributoValor_Atributo_IdAtributo",
                table: "AtributoValor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoAtributoVariante_AtributoValor_IdAtributoValor",
                table: "ProductoAtributoVariante");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoAtributoVariante_ProductoVariante_IdProductoVariante",
                table: "ProductoAtributoVariante");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoVariante_Productos_IdProducto",
                table: "ProductoVariante");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductoVariante",
                table: "ProductoVariante");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductoAtributoVariante",
                table: "ProductoAtributoVariante");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AtributoValor",
                table: "AtributoValor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Atributo",
                table: "Atributo");

            migrationBuilder.RenameTable(
                name: "ProductoVariante",
                newName: "ProductoVariantes");

            migrationBuilder.RenameTable(
                name: "ProductoAtributoVariante",
                newName: "productoAtributoVariantes");

            migrationBuilder.RenameTable(
                name: "AtributoValor",
                newName: "atributoValores");

            migrationBuilder.RenameTable(
                name: "Atributo",
                newName: "Atributos");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoVariante_Precio_IdProducto",
                table: "ProductoVariantes",
                newName: "IX_ProductoVariantes_Precio_IdProducto");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoVariante_IdProducto",
                table: "ProductoVariantes",
                newName: "IX_ProductoVariantes_IdProducto");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoAtributoVariante_IdAtributoValor",
                table: "productoAtributoVariantes",
                newName: "IX_productoAtributoVariantes_IdAtributoValor");

            migrationBuilder.RenameIndex(
                name: "IX_AtributoValor_IdAtributo_Valor",
                table: "atributoValores",
                newName: "IX_atributoValores_IdAtributo_Valor");

            migrationBuilder.RenameIndex(
                name: "IX_Atributo_Nombre",
                table: "Atributos",
                newName: "IX_Atributos_Nombre");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductoVariantes",
                table: "ProductoVariantes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productoAtributoVariantes",
                table: "productoAtributoVariantes",
                columns: new[] { "IdProductoVariante", "IdAtributoValor" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_atributoValores",
                table: "atributoValores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Atributos",
                table: "Atributos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_atributoValores_Atributos_IdAtributo",
                table: "atributoValores",
                column: "IdAtributo",
                principalTable: "Atributos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_productoAtributoVariantes_ProductoVariantes_IdProductoVariante",
                table: "productoAtributoVariantes",
                column: "IdProductoVariante",
                principalTable: "ProductoVariantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_productoAtributoVariantes_atributoValores_IdAtributoValor",
                table: "productoAtributoVariantes",
                column: "IdAtributoValor",
                principalTable: "atributoValores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoVariantes_Productos_IdProducto",
                table: "ProductoVariantes",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_atributoValores_Atributos_IdAtributo",
                table: "atributoValores");

            migrationBuilder.DropForeignKey(
                name: "FK_productoAtributoVariantes_ProductoVariantes_IdProductoVariante",
                table: "productoAtributoVariantes");

            migrationBuilder.DropForeignKey(
                name: "FK_productoAtributoVariantes_atributoValores_IdAtributoValor",
                table: "productoAtributoVariantes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoVariantes_Productos_IdProducto",
                table: "ProductoVariantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductoVariantes",
                table: "ProductoVariantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productoAtributoVariantes",
                table: "productoAtributoVariantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_atributoValores",
                table: "atributoValores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Atributos",
                table: "Atributos");

            migrationBuilder.RenameTable(
                name: "ProductoVariantes",
                newName: "ProductoVariante");

            migrationBuilder.RenameTable(
                name: "productoAtributoVariantes",
                newName: "ProductoAtributoVariante");

            migrationBuilder.RenameTable(
                name: "atributoValores",
                newName: "AtributoValor");

            migrationBuilder.RenameTable(
                name: "Atributos",
                newName: "Atributo");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoVariantes_Precio_IdProducto",
                table: "ProductoVariante",
                newName: "IX_ProductoVariante_Precio_IdProducto");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoVariantes_IdProducto",
                table: "ProductoVariante",
                newName: "IX_ProductoVariante_IdProducto");

            migrationBuilder.RenameIndex(
                name: "IX_productoAtributoVariantes_IdAtributoValor",
                table: "ProductoAtributoVariante",
                newName: "IX_ProductoAtributoVariante_IdAtributoValor");

            migrationBuilder.RenameIndex(
                name: "IX_atributoValores_IdAtributo_Valor",
                table: "AtributoValor",
                newName: "IX_AtributoValor_IdAtributo_Valor");

            migrationBuilder.RenameIndex(
                name: "IX_Atributos_Nombre",
                table: "Atributo",
                newName: "IX_Atributo_Nombre");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductoVariante",
                table: "ProductoVariante",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductoAtributoVariante",
                table: "ProductoAtributoVariante",
                columns: new[] { "IdProductoVariante", "IdAtributoValor" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AtributoValor",
                table: "AtributoValor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Atributo",
                table: "Atributo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AtributoValor_Atributo_IdAtributo",
                table: "AtributoValor",
                column: "IdAtributo",
                principalTable: "Atributo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoAtributoVariante_AtributoValor_IdAtributoValor",
                table: "ProductoAtributoVariante",
                column: "IdAtributoValor",
                principalTable: "AtributoValor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoAtributoVariante_ProductoVariante_IdProductoVariante",
                table: "ProductoAtributoVariante",
                column: "IdProductoVariante",
                principalTable: "ProductoVariante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoVariante_Productos_IdProducto",
                table: "ProductoVariante",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
