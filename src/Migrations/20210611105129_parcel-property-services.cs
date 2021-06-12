using Microsoft.EntityFrameworkCore.Migrations;

namespace Funpoly.Migrations
{
    public partial class parcelpropertyservices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BuffetServiceAvailable",
                table: "ParcelProperty",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ParkingServiceAvailable",
                table: "ParcelProperty",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PoolServiceAvailable",
                table: "ParcelProperty",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WifiServiceAvailable",
                table: "ParcelProperty",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuffetServiceAvailable",
                table: "ParcelProperty");

            migrationBuilder.DropColumn(
                name: "ParkingServiceAvailable",
                table: "ParcelProperty");

            migrationBuilder.DropColumn(
                name: "PoolServiceAvailable",
                table: "ParcelProperty");

            migrationBuilder.DropColumn(
                name: "WifiServiceAvailable",
                table: "ParcelProperty");
        }
    }
}
