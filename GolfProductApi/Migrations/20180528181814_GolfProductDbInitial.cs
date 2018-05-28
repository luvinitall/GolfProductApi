using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GolfProductApi.Migrations
{
    public partial class GolfProductDbInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    CatalogId = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.CatalogId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "CatalogCategory",
                columns: table => new
                {
                    CatalogId = table.Column<short>(nullable: false),
                    CategoryId = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogCategory", x => new { x.CatalogId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CatalogCategory_Catalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "CatalogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    FamilyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    CategoryId = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.FamilyId);
                    table.ForeignKey(
                        name: "FK_Family_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Sku = table.Column<string>(maxLength: 18, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    Gender = table.Column<byte>(nullable: false),
                    Hand = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Family_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Family",
                        principalColumn: "FamilyId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Family",
                columns: new[] { "FamilyId", "CategoryId", "Description" },
                values: new object[,]
                {
                    { 1, (short)1, "Epic Drivers" },
                    { 2, (short)1, "Rogue Drivers" },
                    { 3, (short)2, "Epic Irons" },
                    { 4, (short)2, "Rogue Irons" },
                    { 5, (short)2, "Empty Family" },
                    { 6, (short)3, "Toulon Putter" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "FamilyId", "Gender", "Hand", "Sku" },
                values: new object[,]
                {
                    { 1, "Rogue 3 Iron", 4, (byte)1, (byte)1, "Rogue3IronMR" },
                    { 22, "Rogue 5 Iron", 4, (byte)2, (byte)2, "Rogue5IronLW" },
                    { 21, "Rogue 5-P Iron", 4, (byte)1, (byte)2, "Rogue5PIronLM" },
                    { 20, "Rogue 9 Iron", 4, (byte)1, (byte)2, "Rogue9IronLM" },
                    { 19, "Rogue 8 Iron", 4, (byte)1, (byte)2, "Rogue8IronLM" },
                    { 18, "Rogue 7 Iron", 4, (byte)1, (byte)2, "Rogue7IronLM" },
                    { 17, "Rogue 6 Iron", 4, (byte)1, (byte)2, "Rogue6IronLM" },
                    { 16, "Rogue 5 Iron", 4, (byte)1, (byte)2, "Rogue5IronLM" },
                    { 15, "Rogue 5-P Iron", 4, (byte)2, (byte)1, "Rogue5PIronWR" },
                    { 14, "Rogue 9 Iron", 4, (byte)2, (byte)1, "Rogue9IronWR" },
                    { 13, "Rogue 8 Iron", 4, (byte)2, (byte)1, "Rogue8IronWR" },
                    { 12, "Rogue 7 Iron", 4, (byte)2, (byte)1, "Rogue7IronWR" },
                    { 11, "Rogue 6 Iron", 4, (byte)2, (byte)1, "Rogue6IronWR" },
                    { 10, "Rogue 5 Iron", 4, (byte)2, (byte)1, "Rogue5IronWR" },
                    { 9, "Rogue 5-P Iron", 4, (byte)1, (byte)1, "Rogue5PIronMR" },
                    { 8, "Rogue 3-P Iron", 4, (byte)1, (byte)1, "Rogue3PIronMR" },
                    { 7, "Rogue 9 Iron", 4, (byte)1, (byte)1, "Rogue9IronMR" },
                    { 6, "Rogue 8 Iron", 4, (byte)1, (byte)1, "Rogue8IronMR" },
                    { 5, "Rogue 7 Iron", 4, (byte)1, (byte)1, "Rogue7IronMR" },
                    { 4, "Rogue 6 Iron", 4, (byte)1, (byte)1, "Rogue6IronMR" },
                    { 3, "Rogue 5 Iron", 4, (byte)1, (byte)1, "Rogue5IronMR" },
                    { 2, "Rogue 4 Iron", 4, (byte)1, (byte)1, "Rogue4IronMR" },
                    { 23, "Rogue 7 Iron", 4, (byte)2, (byte)2, "Rogue7IronLW" },
                    { 24, "Rogue 9 Iron", 4, (byte)2, (byte)2, "Rogue9IronLW" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogCategory_CategoryId",
                table: "CatalogCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "uidx_Catalog_Description",
                table: "Catalogs",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uidx_Category_Description",
                table: "Category",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Family_CategoryId",
                table: "Family",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "uidx_Family_Description",
                table: "Family",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_FamilyId",
                table: "Products",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "uidx_Product_Sku",
                table: "Products",
                column: "Sku",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogCategory");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.DropTable(
                name: "Family");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
