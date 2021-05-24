using Microsoft.EntityFrameworkCore.Migrations;

namespace Funpoly.Migrations
{
    public partial class parcelstaxesprices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelBuilt",
                table: "Parcels");

            migrationBuilder.AddColumn<int>(
                name: "HotelPrice",
                table: "Parcels",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelPrice",
                table: "Parcels");

            migrationBuilder.AddColumn<bool>(
                name: "HotelBuilt",
                table: "Parcels",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
