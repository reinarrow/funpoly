using Microsoft.EntityFrameworkCore.Migrations;

namespace Funpoly.Migrations
{
    public partial class multigames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Teams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Games",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_GameId",
                table: "Teams",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Games_GameId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_GameId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Games");
        }
    }
}
