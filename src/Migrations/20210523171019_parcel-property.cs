using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Funpoly.Migrations
{
    public partial class parcelproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_Teams_TeamId",
                table: "Parcels");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_TeamId",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Parcels");

            migrationBuilder.AddColumn<int>(
                name: "TwoHotelsTax",
                table: "Parcels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ParcelProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    ParcelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParcelProperty_Parcels_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParcelProperty_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParcelProperty_ParcelId",
                table: "ParcelProperty",
                column: "ParcelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParcelProperty_TeamId",
                table: "ParcelProperty",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParcelProperty");

            migrationBuilder.DropColumn(
                name: "TwoHotelsTax",
                table: "Parcels");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Parcels",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_TeamId",
                table: "Parcels",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_Teams_TeamId",
                table: "Parcels",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
