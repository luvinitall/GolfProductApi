using Microsoft.EntityFrameworkCore.Migrations;

namespace GolfProductApi.Migrations
{
    public partial class GolfProductDbAddSeedDataForRogueIronProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "uidx_Product_Sku",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "uidx_Product_Sku",
                table: "Products",
                column: "Sku",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "uidx_Product_Sku",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "uidx_Product_Sku",
                table: "Products",
                column: "Description",
                unique: true);
        }
    }
}
