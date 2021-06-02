using Microsoft.EntityFrameworkCore.Migrations;

namespace Funpoly.Migrations
{
    public partial class multipleparcelproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParcelProperty_ParcelId",
                table: "ParcelProperty");

            migrationBuilder.AddColumn<bool>(
                name: "HotelBuilt",
                table: "ParcelProperty",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ParcelProperty_ParcelId",
                table: "ParcelProperty",
                column: "ParcelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParcelProperty_ParcelId",
                table: "ParcelProperty");

            migrationBuilder.DropColumn(
                name: "HotelBuilt",
                table: "ParcelProperty");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelProperty_ParcelId",
                table: "ParcelProperty",
                column: "ParcelId",
                unique: true);
        }
    }
}
