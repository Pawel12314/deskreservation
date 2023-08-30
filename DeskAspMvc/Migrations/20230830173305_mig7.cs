using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskAspMvc.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nr",
                table: "Desk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "nr",
                table: "Desk",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
