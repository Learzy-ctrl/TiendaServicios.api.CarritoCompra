using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaServicios.api.CarritoCompra.Migrations
{
    /// <inheritdoc />
    public partial class mysql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "carritoSesions",
                columns: table => new
                {
                    CarritoSesionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carritoSesions", x => x.CarritoSesionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "carritoSesionDetalles",
                columns: table => new
                {
                    CarritoSessionDetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ProductoSeleccionado = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
