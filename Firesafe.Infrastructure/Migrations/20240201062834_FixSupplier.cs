using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSupplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Provinces_ProvinceId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_ProvinceId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Suppliers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Suppliers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ProvinceId",
                table: "Suppliers",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Provinces_ProvinceId",
                table: "Suppliers",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "ProvinceId");
        }
    }
}
