using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorInCarritoItemsInProductosInProductosVariante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoItems_Productos_IdProducto",
                table: "CarritoItems");

            migrationBuilder.RenameColumn(
                name: "IdProducto",
                table: "CarritoItems",
                newName: "IdProductoVariante");

            migrationBuilder.RenameIndex(
                name: "IX_CarritoItems_IdProducto",
                table: "CarritoItems",
                newName: "IX_CarritoItems_IdProductoVariante");

            migrationBuilder.RenameIndex(
                name: "IX_CarritoItems_IdCarrito_IdProducto",
                table: "CarritoItems",
                newName: "IX_CarritoItems_IdCarrito_IdProductoVariante");

            migrationBuilder.AlterColumn<int>(
                name: "IdModelo",
                table: "Productos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoItems_ProductoVariantes_IdProductoVariante",
                table: "CarritoItems",
                column: "IdProductoVariante",
                principalTable: "ProductoVariantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarritoItems_ProductoVariantes_IdProductoVariante",
                table: "CarritoItems");

            migrationBuilder.RenameColumn(
                name: "IdProductoVariante",
                table: "CarritoItems",
                newName: "IdProducto");

            migrationBuilder.RenameIndex(
                name: "IX_CarritoItems_IdProductoVariante",
                table: "CarritoItems",
                newName: "IX_CarritoItems_IdProducto");

            migrationBuilder.RenameIndex(
                name: "IX_CarritoItems_IdCarrito_IdProductoVariante",
                table: "CarritoItems",
                newName: "IX_CarritoItems_IdCarrito_IdProducto");

            migrationBuilder.AlterColumn<int>(
                name: "IdModelo",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarritoItems_Productos_IdProducto",
                table: "CarritoItems",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
