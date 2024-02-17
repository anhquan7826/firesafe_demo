using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewspaperCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewspaperCategories",
                columns: table => new
                {
                    NewspaperCategoryId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewspaperCategories", x => x.NewspaperCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "NewspaperCategoryJunctions",
                columns: table => new
                {
                    NewspaperId = table.Column<Guid>(type: "uuid", nullable: false),
                    NewspaperCategoryId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewspaperCategoryJunctions", x => new { x.NewspaperId, x.NewspaperCategoryId });
                    table.ForeignKey(
                        name: "FK_NewspaperCategoryJunctions_NewspaperCategories_NewspaperCat~",
                        column: x => x.NewspaperCategoryId,
                        principalTable: "NewspaperCategories",
                        principalColumn: "NewspaperCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewspaperCategoryJunctions_Newspapers_NewspaperId",
                        column: x => x.NewspaperId,
                        principalTable: "Newspapers",
                        principalColumn: "NewspaperId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "NewspaperCategories",
                column: "NewspaperCategoryId",
                values: new object[]
                {
                    "newspaper_category_field",
                    "newspaper_category_product_category"
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewspaperCategoryJunctions_NewspaperCategoryId",
                table: "NewspaperCategoryJunctions",
                column: "NewspaperCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewspaperCategoryJunctions");

            migrationBuilder.DropTable(
                name: "NewspaperCategories");
        }
    }
}
