using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaServicios.api.CarritoCompra.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carritoSesions",
                columns: table => new
                {
                    CarritoSesionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carritoSesions", x => x.CarritoSesionId);
                });

            migrationBuilder.CreateTable(
                name: "carritoSesionDetalles",
                columns: table => new
                {
                    CarritoSessionDetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductoSeleccionado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarritoSesionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carritoSesionDetalles", x => x.CarritoSessionDetalleId);
                    table.ForeignKey(
                        name: "FK_carritoSesionDetalles_carritoSesions_CarritoSesionId",
                        column: x => x.CarritoSesionId,
                        principalTable: "carritoSesions",
                        principalColumn: "CarritoSesionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carritoSesionDetalles_CarritoSesionId",
                table: "carritoSesionDetalles",
                column: "CarritoSesionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carritoSesionDetalles");

            migrationBuilder.DropTable(
                name: "carritoSesions");
        }
    }
}
