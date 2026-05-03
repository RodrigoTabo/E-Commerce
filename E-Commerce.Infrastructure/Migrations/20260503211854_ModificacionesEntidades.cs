using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionesEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoOrden",
                table: "OrdenItems");

            migrationBuilder.AddColumn<int>(
                name: "AlturaSnapshot",
                table: "Ordenes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CalleSnapshot",
                table: "Ordenes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CiudadSnapshot",
                table: "Ordenes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodigoPostalSnapshot",
                table: "Ordenes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlturaSnapshot",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "CalleSnapshot",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "CiudadSnapshot",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "CodigoPostalSnapshot",
                table: "Ordenes");

            migrationBuilder.AddColumn<string>(
                name: "EstadoOrden",
                table: "OrdenItems",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
