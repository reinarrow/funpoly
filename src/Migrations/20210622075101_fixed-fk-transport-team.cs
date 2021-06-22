using Microsoft.EntityFrameworkCore.Migrations;

namespace Funpoly.Migrations
{
    public partial class fixedfktransportteam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_TransportId",
                table: "Teams");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TransportId",
                table: "Teams",
                column: "TransportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_TransportId",
                table: "Teams");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TransportId",
                table: "Teams",
                column: "TransportId",
                unique: true);
        }
    }
}
