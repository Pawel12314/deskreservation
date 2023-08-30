using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskAspMvc.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ReservationItem",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reservation",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Location",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Desk",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "ReservationItem",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Reservation",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Location",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Desk",
                newName: "Id");
        }
    }
}
