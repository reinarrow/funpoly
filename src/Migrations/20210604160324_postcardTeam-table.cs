using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Funpoly.Migrations
{
    public partial class postcardTeamtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostcardTeam_Postcards_PostcardsId",
                table: "PostcardTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_PostcardTeam_Teams_TeamsId",
                table: "PostcardTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostcardTeam",
                table: "PostcardTeam");

            migrationBuilder.RenameColumn(
                name: "TeamsId",
                table: "PostcardTeam",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "PostcardsId",
                table: "PostcardTeam",
                newName: "PostcardId");

            migrationBuilder.RenameIndex(
                name: "IX_PostcardTeam_TeamsId",
                table: "PostcardTeam",
                newName: "IX_PostcardTeam_TeamId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PostcardTeam",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostcardTeam",
                table: "PostcardTeam",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PostcardTeam_PostcardId",
                table: "PostcardTeam",
                column: "PostcardId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostcardTeam_Postcards_PostcardId",
                table: "PostcardTeam",
                column: "PostcardId",
                principalTable: "Postcards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostcardTeam_Teams_TeamId",
                table: "PostcardTeam",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostcardTeam_Postcards_PostcardId",
                table: "PostcardTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_PostcardTeam_Teams_TeamId",
                table: "PostcardTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostcardTeam",
                table: "PostcardTeam");

            migrationBuilder.DropIndex(
                name: "IX_PostcardTeam_PostcardId",
                table: "PostcardTeam");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostcardTeam");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "PostcardTeam",
                newName: "TeamsId");

            migrationBuilder.RenameColumn(
                name: "PostcardId",
                table: "PostcardTeam",
                newName: "PostcardsId");

            migrationBuilder.RenameIndex(
                name: "IX_PostcardTeam_TeamId",
                table: "PostcardTeam",
                newName: "IX_PostcardTeam_TeamsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostcardTeam",
                table: "PostcardTeam",
                columns: new[] { "PostcardsId", "TeamsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostcardTeam_Postcards_PostcardsId",
                table: "PostcardTeam",
                column: "PostcardsId",
                principalTable: "Postcards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostcardTeam_Teams_TeamsId",
                table: "PostcardTeam",
                column: "TeamsId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
