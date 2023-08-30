using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskAspMvc.Migrations
{
    /// <inheritdoc />
    public partial class mig31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desk_Reservation_reservationId",
                table: "Desk");

            migrationBuilder.DropIndex(
                name: "IX_Desk_reservationId",
                table: "Desk");

            migrationBuilder.DropColumn(
                name: "reservationId",
                table: "Desk");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_deskId",
                table: "Reservation",
                column: "deskId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Desk_deskId",
                table: "Reservation",
                column: "deskId",
                principalTable: "Desk",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Desk_deskId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_deskId",
                table: "Reservation");

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
    }
}
