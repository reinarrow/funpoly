using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Funpoly.Migrations
{
    public partial class settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InitialCash = table.Column<decimal>(type: "numeric", nullable: false),
                    LapPaymentAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    SpeedFineTax = table.Column<decimal>(type: "numeric", nullable: false),
                    SpeedReprimandCount = table.Column<int>(type: "integer", nullable: false),
                    RawPropertyTaxPriceMultiplier = table.Column<decimal>(type: "numeric", nullable: false),
                    RepurchaseMultiplier = table.Column<decimal>(type: "numeric", nullable: false),
                    FourPropertiesTaxMultiplier = table.Column<decimal>(type: "numeric", nullable: false),
                    HotelPropertyTaxMultiplier = table.Column<decimal>(type: "numeric", nullable: false),
                    ServicePropertyTaxIncrement = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
