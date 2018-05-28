using Microsoft.EntityFrameworkCore.Migrations;

namespace GolfProductApi.Migrations
{
    public partial class GolfProductDbAddSeedDataForCatalog_Category_Family : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "CatalogId", "Description" },
                values: new object[,]
                {
                    { (short)1, "US Catalog" },
                    { (short)2, "Japan Catalog" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Description" },
                values: new object[,]
                {
                    { (short)1, "Woods" },
                    { (short)2, "Irons" },
                    { (short)3, "Putters" }
                });

            migrationBuilder.InsertData(
                table: "CatalogCategory",
                columns: new[] { "CatalogId", "CategoryId" },
                values: new object[,]
                {
                    { (short)1, (short)1 },
                    { (short)2, (short)1 },
                    { (short)1, (short)2 },
                    { (short)2, (short)2 },
                    { (short)1, (short)3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CatalogCategory",
                keyColumns: new[] { "CatalogId", "CategoryId" },
                keyValues: new object[] { (short)1, (short)1 });

            migrationBuilder.DeleteData(
                table: "CatalogCategory",
                keyColumns: new[] { "CatalogId", "CategoryId" },
                keyValues: new object[] { (short)1, (short)2 });

            migrationBuilder.DeleteData(
                table: "CatalogCategory",
                keyColumns: new[] { "CatalogId", "CategoryId" },
                keyValues: new object[] { (short)1, (short)3 });

            migrationBuilder.DeleteData(
                table: "CatalogCategory",
                keyColumns: new[] { "CatalogId", "CategoryId" },
                keyValues: new object[] { (short)2, (short)1 });

            migrationBuilder.DeleteData(
                table: "CatalogCategory",
                keyColumns: new[] { "CatalogId", "CategoryId" },
                keyValues: new object[] { (short)2, (short)2 });

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "CatalogId",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "CatalogId",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: (short)3);
        }
    }
}
