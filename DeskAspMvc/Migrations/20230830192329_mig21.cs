using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskAspMvc.Migrations
{
    /// <inheritdoc />
    public partial class mig21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dueInMilis",
                table: "Reservation");

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "Reservation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "Reservation");

            migrationBuilder.AddColumn<long>(
                name: "dueInMilis",
                table: "Reservation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
