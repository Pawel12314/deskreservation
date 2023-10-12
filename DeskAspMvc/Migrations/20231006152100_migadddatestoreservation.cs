using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskAspMvc.Migrations
{
    /// <inheritdoc />
    public partial class migadddatestoreservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "begindate",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "enddate",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "begindate",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "enddate",
                table: "Reservation");
        }
    }
}
