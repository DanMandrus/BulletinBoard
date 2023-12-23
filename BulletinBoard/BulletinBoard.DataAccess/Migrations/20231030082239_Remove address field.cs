using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulletinBoard.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Removeaddressfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "announcements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "announcements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
