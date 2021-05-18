using Microsoft.EntityFrameworkCore.Migrations;

namespace Funpoly.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Player 1" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Player 2" });

            migrationBuilder.InsertData(
                table: "Player",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Player 3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
