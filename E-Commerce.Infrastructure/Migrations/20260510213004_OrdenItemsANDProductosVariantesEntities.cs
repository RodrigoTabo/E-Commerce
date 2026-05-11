using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrdenItemsANDProductosVariantesEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenItems_Productos_IdProducto",
                table: "OrdenItems");

            migrationBuilder.RenameColumn(
                name: "IdProducto",
                table: "OrdenItems",
                newName: "IdProductoVariante");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenItems_IdProducto",
                table: "OrdenItems",
                newName: "IX_OrdenItems_IdProductoVariante");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenItems_ProductoVariantes_IdProductoVariante",
                table: "OrdenItems",
                column: "IdProductoVariante",
                principalTable: "ProductoVariantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdenItems_ProductoVariantes_IdProductoVariante",
                table: "OrdenItems");

            migrationBuilder.RenameColumn(
                name: "IdProductoVariante",
                table: "OrdenItems",
                newName: "IdProducto");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenItems_IdProductoVariante",
                table: "OrdenItems",
                newName: "IX_OrdenItems_IdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenItems_Productos_IdProducto",
                table: "OrdenItems",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
