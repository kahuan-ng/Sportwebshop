using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Wba.PE02_Webshop.Web.Migrations
{
    /// <inheritdoc />
    public partial class InititalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BrandLogo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attribute = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AttributeCategory = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SportProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportProducts_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SportProducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "FeatureSportProduct",
                columns: table => new
                {
                    FeaturesId = table.Column<int>(type: "int", nullable: false),
                    SportProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureSportProduct", x => new { x.FeaturesId, x.SportProductsId });
                    table.ForeignKey(
                        name: "FK_FeatureSportProduct_Features_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureSportProduct_SportProducts_SportProductsId",
                        column: x => x.SportProductsId,
                        principalTable: "SportProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BrandLogo", "BrandName" },
                values: new object[,]
                {
                    { 1, "nike-logo.png", "Nike" },
                    { 2, "Adidas_Logo.png", "Adidas" },
                    { 3, "puma-logo.png", "Puma" },
                    { 4, "Reebok-logo.png", "Reebok" },
                    { 5, "UnderArmour-logo.png", "Under Armour" },
                    { 6, "NewBalance-logo.png", "New Balance" },
                    { 7, "Asics-logo.png", "Asics" },
                    { 8, "Fila-logo.png", "Fila" },
                    { 9, "Naak-logo.png", "Naak" },
                    { 10, "lucovitaal-logo.png", "Lucavitaal" },
                    { 11, "optimum-nutrition-logo.png", "Optimum Nutrition" },
                    { 12, "action-logo.png", "Action" },
                    { 13, "Apherma-Logo.png", "Apherma" },
                    { 14, "fitnessspeed-logo.png", "Fitness Speed" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Clothing" },
                    { 2, "Nutrition" },
                    { 3, "Equipment" }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Attribute", "AttributeCategory" },
                values: new object[,]
                {
                    { 1, "Black", "Color" },
                    { 2, "White", "Color" },
                    { 3, "Red", "Color" },
                    { 4, "Blue", "Color" },
                    { 5, "Green", "Color" },
                    { 6, "Pink", "Color" },
                    { 7, "Purple", "Color" },
                    { 8, "Orange", "Color" },
                    { 9, "Yellow", "Color" },
                    { 10, "XS", "Size" },
                    { 11, "S", "Size" },
                    { 12, "M", "Size" },
                    { 13, "L", "Size" },
                    { 14, "XL", "Size" },
                    { 15, "XXL", "Size" },
                    { 16, "Male", "Audience" },
                    { 17, "Female", "Audience" },
                    { 18, "Kids", "Audience" },
                    { 19, "Carbohydrates", "Nutrition" },
                    { 20, "Magnesium", "Nutrition" },
                    { 21, "Protein", "Nutrition" },
                    { 22, "Protein Powder", "Nutrition" },
                    { 23, "Weights", "Equipment" },
                    { 24, "Machine", "Equipment" },
                    { 25, "Recovery", "Equipment" },
                    { 26, "Sport Equipment", "Equipment" },
                    { 27, "Cardio", "Equipment" },
                    { 28, "Supplements", "Nutrition" },
                    { 29, "Promo", "Promo" }
                });

            migrationBuilder.InsertData(
                table: "SportProducts",
                columns: new[] { "Id", "BrandId", "CategoryId", "Name", "Price", "ProductImage", "Rating" },
                values: new object[,]
                {
                    { 1, 2, 1, "Adidas Men Hoody", 41.99m, "Clothing/WBA_PE01_addidas_sporthoodie.png", 4 },
                    { 2, 3, 1, "PUMA Woman Leggings", 29.99m, "Clothing/Wba_PE01_img_broek.png", 4 },
                    { 3, 1, 1, "Nike Running Shoes", 199.99m, "Clothing/WBA_PE01_img_nikeRunningShoes.png", 4 },
                    { 4, 9, 2, "Naak Boost Energygel", 1.99m, "Nutrition/WBa_PE01_img_energiegel.png", 1 },
                    { 5, 10, 2, "Lucivaal 400mg magnesiums pills", 7.99m, "Nutrition/WBA_PE01_img_magnesiumpillen.png", 4 },
                    { 6, 11, 2, "Optimum Nutrition Gold Standard 2.27kg Whey Protein Powder", 63.99m, "Nutrition/WBA_PE01_img_proteinpowder.png", 5 },
                    { 7, 12, 3, "5kg dumbbell", 9.99m, "Equipment/WBA_PE01_img_db.png", 3 },
                    { 8, 13, 3, "Muscle Massage Gun", 81.99m, "Equipment/WBa_PE01_img_massagemachine.png", 5 },
                    { 9, 14, 3, "2.7m jumprope", 4.99m, "Equipment/WBa_PE01_img_springtouw.png", 3 }
                });

            migrationBuilder.InsertData(
                table: "FeatureSportProduct",
                columns: new[] { "FeaturesId", "SportProductsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 3 },
                    { 6, 2 },
                    { 6, 3 },
                    { 9, 3 },
                    { 10, 2 },
                    { 10, 3 },
                    { 12, 1 },
                    { 16, 1 },
                    { 17, 2 },
                    { 18, 3 },
                    { 19, 4 },
                    { 20, 5 },
                    { 21, 6 },
                    { 22, 6 },
                    { 24, 7 },
                    { 24, 8 },
                    { 25, 8 },
                    { 26, 9 },
                    { 27, 8 },
                    { 28, 4 },
                    { 28, 5 },
                    { 28, 7 },
                    { 29, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureSportProduct_SportProductsId",
                table: "FeatureSportProduct",
                column: "SportProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_SportProducts_BrandId",
                table: "SportProducts",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_SportProducts_CategoryId",
                table: "SportProducts",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureSportProduct");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "SportProducts");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
