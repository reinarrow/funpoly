using Microsoft.EntityFrameworkCore.Migrations;

namespace Funpoly.Migrations
{
    public partial class consecutivesixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConsecutiveTwelves",
                table: "Teams",
                newName: "ConsecutiveSixes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConsecutiveSixes",
                table: "Teams",
                newName: "ConsecutiveTwelves");
        }
    }
}
