using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Origin_OriginId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Origin",
                table: "Origin");

            migrationBuilder.DropColumn(
                name: "DistrictOriginId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "HasWarranty",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProvinceOriginId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Specification",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WardOriginId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Origin",
                newName: "Origins");

            migrationBuilder.RenameColumn(
                name: "IsNew",
                table: "Products",
                newName: "Available");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "ShortDescription");

            migrationBuilder.AlterColumn<string>(
                name: "Packaging",
                table: "Products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "OriginId",
                table: "Products",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HasSample",
                table: "Products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "Faq",
                table: "Products",
                type: "character varying(10000)",
                maxLength: 10000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10000)",
                oldMaxLength: 10000);

            migrationBuilder.AddColumn<string>(
                name: "Accessories",
                table: "Products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalSpecification",
                table: "Products",
                type: "character varying(8000)",
                maxLength: 8000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Applications",
                table: "Products",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FireResistant",
                table: "Products",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostSupport",
                table: "Products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionRate",
                table: "Products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Shape",
                table: "Products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Structure",
                table: "Products",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Volume",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Warranty",
                table: "Products",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "WaterResistant",
                table: "Products",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Origins",
                table: "Origins",
                column: "OriginId");

            migrationBuilder.CreateTable(
                name: "ProductCertifications",
                columns: table => new
                {
                    ProductCertificateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCertifications", x => x.ProductCertificateId);
                    table.ForeignKey(
                        name: "FK_ProductCertifications_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCertifications_ProductId",
                table: "ProductCertifications",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Origins_OriginId",
                table: "Products",
                column: "OriginId",
                principalTable: "Origins",
                principalColumn: "OriginId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Origins_OriginId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductCertifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Origins",
                table: "Origins");

            migrationBuilder.DropColumn(
                name: "Accessories",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AdditionalSpecification",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Applications",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FireResistant",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PostSupport",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductionRate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Shape",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Structure",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Warranty",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WaterResistant",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Origins",
                newName: "Origin");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Available",
                table: "Products",
                newName: "IsNew");

            migrationBuilder.AlterColumn<string>(
                name: "Packaging",
                table: "Products",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "OriginId",
                table: "Products",
                type: "character varying(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<bool>(
                name: "HasSample",
                table: "Products",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Faq",
                table: "Products",
                type: "character varying(10000)",
                maxLength: 10000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(10000)",
                oldMaxLength: 10000,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictOriginId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasWarranty",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceOriginId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Shipping",
                table: "Products",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Specification",
                table: "Products",
                type: "character varying(8000)",
                maxLength: 8000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WardOriginId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Origin",
                table: "Origin",
                column: "OriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Origin_OriginId",
                table: "Products",
                column: "OriginId",
                principalTable: "Origin",
                principalColumn: "OriginId");
        }
    }
}
