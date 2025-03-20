using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Catalog.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.CreateTable(
                name: "catalog_brand",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalog_brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "catalog_category",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalog_category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_catalog_category_catalog_category_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "catalog",
                        principalTable: "catalog_category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "catalog_item",
                schema: "catalog",
                columns: table => new
                {
                    Slug = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    AvailableStock = table.Column<int>(type: "integer", nullable: false),
                    CatalogBrandId = table.Column<int>(type: "integer", nullable: false),
                    CatalogCategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalog_item", x => x.Slug);
                    table.ForeignKey(
                        name: "FK_catalog_item_catalog_brand_CatalogBrandId",
                        column: x => x.CatalogBrandId,
                        principalSchema: "catalog",
                        principalTable: "catalog_brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_catalog_item_catalog_category_CatalogCategoryId",
                        column: x => x.CatalogCategoryId,
                        principalSchema: "catalog",
                        principalTable: "catalog_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_catalog_category_ParentId",
                schema: "catalog",
                table: "catalog_category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_catalog_item_CatalogBrandId",
                schema: "catalog",
                table: "catalog_item",
                column: "CatalogBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_catalog_item_CatalogCategoryId",
                schema: "catalog",
                table: "catalog_item",
                column: "CatalogCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "catalog_item",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "catalog_brand",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "catalog_category",
                schema: "catalog");
        }
    }
}
