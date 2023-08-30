using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskAspMvc.Migrations
{
    /// <inheritdoc />
    public partial class mig12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Location_locationId",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "ReservationItem");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_locationId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "owner",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "locationId",
                table: "Reservation",
                newName: "deskId");

            migrationBuilder.AddColumn<bool>(
                name: "available",
                table: "Desk",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "reservationId",
                table: "Desk",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Desk_reservationId",
                table: "Desk",
                column: "reservationId",
                unique: true,
                filter: "[reservationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Desk_Reservation_reservationId",
                table: "Desk",
                column: "reservationId",
                principalTable: "Reservation",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desk_Reservation_reservationId",
                table: "Desk");

            migrationBuilder.DropIndex(
                name: "IX_Desk_reservationId",
                table: "Desk");

            migrationBuilder.DropColumn(
                name: "available",
                table: "Desk");

            migrationBuilder.DropColumn(
                name: "reservationId",
                table: "Desk");

            migrationBuilder.RenameColumn(
                name: "deskId",
                table: "Reservation",
                newName: "locationId");

            migrationBuilder.AddColumn<string>(
                name: "owner",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ReservationItem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deskId = table.Column<int>(type: "int", nullable: false),
                    reservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationItem", x => x.id);
                    table.ForeignKey(
                        name: "FK_ReservationItem_Desk_deskId",
                        column: x => x.deskId,
                        principalTable: "Desk",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationItem_Reservation_reservationId",
                        column: x => x.reservationId,
                        principalTable: "Reservation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_locationId",
                table: "Reservation",
                column: "locationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItem_deskId",
                table: "ReservationItem",
                column: "deskId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItem_reservationId",
                table: "ReservationItem",
                column: "reservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Location_locationId",
                table: "Reservation",
                column: "locationId",
                principalTable: "Location",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
