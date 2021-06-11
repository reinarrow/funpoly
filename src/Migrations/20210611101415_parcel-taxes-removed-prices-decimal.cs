using Microsoft.EntityFrameworkCore.Migrations;

namespace Funpoly.Migrations
{
    public partial class parceltaxesremovedpricesdecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelTax",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "RawTax",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "TwoHotelsTax",
                table: "Parcels");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Parcels",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "HotelPrice",
                table: "Parcels",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Parcels",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "HotelPrice",
                table: "Parcels",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<int>(
                name: "HotelTax",
                table: "Parcels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RawTax",
                table: "Parcels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TwoHotelsTax",
                table: "Parcels",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
