using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(type: "uuid", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    City = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    District = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Ward = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AddressDetail = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "Newspapers",
                columns: table => new
                {
                    NewspaperId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EditedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newspapers", x => x.NewspaperId);
                });

            migrationBuilder.CreateTable(
                name: "Origin",
                columns: table => new
                {
                    OriginId = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Origin", x => x.OriginId);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ProvinceId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirebaseId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "NewspaperImages",
                columns: table => new
                {
                    NewspaperImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    NewspaperId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewspaperImages", x => x.NewspaperImageId);
                    table.ForeignKey(
                        name: "FK_NewspaperImages_Newspapers_NewspaperId",
                        column: x => x.NewspaperId,
                        principalTable: "Newspapers",
                        principalColumn: "NewspaperId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true),
                    EstablishedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Rating = table.Column<short>(type: "smallint", nullable: false),
                    Avatar = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Banner = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProvinceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                    table.ForeignKey(
                        name: "FK_Suppliers_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Suppliers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleType });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleType",
                        column: x => x.RoleType,
                        principalTable: "Roles",
                        principalColumn: "Type",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    Specification = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    Model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Brand = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ShippingTime = table.Column<short>(type: "smallint", nullable: false),
                    Packaging = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Shipping = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    HasSample = table.Column<bool>(type: "boolean", nullable: false),
                    IsNew = table.Column<bool>(type: "boolean", nullable: false),
                    HasWarranty = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: true),
                    PriceMax = table.Column<int>(type: "integer", nullable: true),
                    PriceMin = table.Column<int>(type: "integer", nullable: true),
                    Rating = table.Column<short>(type: "smallint", nullable: false),
                    Faq = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProvinceOriginId = table.Column<int>(type: "integer", nullable: false),
                    DistrictOriginId = table.Column<int>(type: "integer", nullable: false),
                    WardOriginId = table.Column<int>(type: "integer", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginId = table.Column<string>(type: "character varying(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Origin_OriginId",
                        column: x => x.OriginId,
                        principalTable: "Origin",
                        principalColumn: "OriginId");
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierContacts",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierContacts", x => new { x.ContactId, x.SupplierId });
                    table.ForeignKey(
                        name: "FK_SupplierContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierContacts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierReviews",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<short>(type: "smallint", nullable: false),
                    Review = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierReviews", x => new { x.UserId, x.SupplierId });
                    table.ForeignKey(
                        name: "FK_SupplierReviews_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFavorites",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFavorites", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductFavorites_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFavorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProductImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<short>(type: "smallint", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ProductImageId);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<short>(type: "smallint", nullable: false),
                    Review = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => new { x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductViewHistories",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductViewHistories", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductViewHistories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductViewHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                column: "CategoryId",
                values: new object[]
                {
                    "category_construction_material",
                    "category_door",
                    "category_electric_system",
                    "category_household_appliance",
                    "category_material",
                    "category_other",
                    "category_paint",
                    "category_pipe_cable"
                });

            migrationBuilder.InsertData(
                table: "Origin",
                columns: new[] { "OriginId", "Name" },
                values: new object[,]
                {
                    { "ad", "Andorra" },
                    { "ae", "United Arab Emirates" },
                    { "af", "Afghanistan" },
                    { "ag", "Antigua And Barbuda" },
                    { "ai", "Anguilla" },
                    { "al", "Albania" },
                    { "am", "Armenia" },
                    { "an", "Netherlands Antilles" },
                    { "ao", "Angola" },
                    { "aq", "Antarctica" },
                    { "ar", "Argentina" },
                    { "as", "American Samoa" },
                    { "at", "Austria" },
                    { "au", "Australia" },
                    { "aw", "Aruba" },
                    { "ax", "Åland Islands" },
                    { "az", "Azerbaijan" },
                    { "ba", "Bosnia And Herzegovina" },
                    { "bb", "Barbados" },
                    { "bd", "Bangladesh" },
                    { "be", "Belgium" },
                    { "bf", "Burkina Faso" },
                    { "bg", "Bulgaria" },
                    { "bh", "Bahrain" },
                    { "bi", "Burundi" },
                    { "bj", "Benin" },
                    { "bm", "Bermuda" },
                    { "bn", "Brunei Darussalam" },
                    { "bo", "Bolivia" },
                    { "br", "Brazil" },
                    { "bs", "Bahamas" },
                    { "bt", "Bhutan" },
                    { "bv", "Bouvet Island" },
                    { "bw", "Botswana" },
                    { "by", "Belarus" },
                    { "bz", "Belize" },
                    { "ca", "Canada" },
                    { "cc", "Cocos (Keeling) Islands" },
                    { "cd", "Congo, The Democratic Republic Of The" },
                    { "cf", "Central African Republic" },
                    { "cg", "Congo" },
                    { "ch", "Switzerland" },
                    { "ci", "Côte D'Ivoire" },
                    { "ck", "Cook Islands" },
                    { "cl", "Chile" },
                    { "cm", "Cameroon" },
                    { "cn", "China" },
                    { "co", "Colombia" },
                    { "cr", "Costa Rica" },
                    { "cs", "Serbia And Montenegro" },
                    { "cu", "Cuba" },
                    { "cv", "Cape Verde" },
                    { "cx", "Christmas Island" },
                    { "cy", "Cyprus" },
                    { "cz", "Czech Republic" },
                    { "de", "Germany" },
                    { "dj", "Djibouti" },
                    { "dk", "Denmark" },
                    { "dm", "Dominica" },
                    { "do", "Dominican Republic" },
                    { "dz", "Algeria" },
                    { "ec", "Ecuador" },
                    { "ee", "Estonia" },
                    { "eg", "Egypt" },
                    { "eh", "Western Sahara" },
                    { "er", "Eritrea" },
                    { "es", "Spain" },
                    { "et", "Ethiopia" },
                    { "fi", "Finland" },
                    { "fj", "Fiji" },
                    { "fk", "Falkland Islands (Malvinas)" },
                    { "fm", "Micronesia, Federated States Of" },
                    { "fo", "Faroe Islands" },
                    { "fr", "France" },
                    { "ga", "Gabon" },
                    { "gb", "United Kingdom" },
                    { "gd", "Grenada" },
                    { "ge", "Georgia" },
                    { "gf", "French Guiana" },
                    { "gh", "Ghana" },
                    { "gi", "Gibraltar" },
                    { "gl", "Greenland" },
                    { "gm", "Gambia" },
                    { "gn", "Guinea" },
                    { "gp", "Guadeloupe" },
                    { "gq", "Equatorial Guinea" },
                    { "gr", "Greece" },
                    { "gs", "South Georgia And The South Sandwich Islands" },
                    { "gt", "Guatemala" },
                    { "gu", "Guam" },
                    { "gw", "Guinea-Bissau" },
                    { "gy", "Guyana" },
                    { "hk", "Hong Kong" },
                    { "hm", "Heard Island And Mcdonald Islands" },
                    { "hn", "Honduras" },
                    { "hr", "Croatia" },
                    { "ht", "Haiti" },
                    { "hu", "Hungary" },
                    { "id", "Indonesia" },
                    { "ie", "Ireland" },
                    { "il", "Israel" },
                    { "in", "India" },
                    { "io", "British Indian Ocean Territory" },
                    { "iq", "Iraq" },
                    { "ir", "Iran, Islamic Republic Of" },
                    { "is", "Iceland" },
                    { "it", "Italy" },
                    { "jm", "Jamaica" },
                    { "jo", "Jordan" },
                    { "jp", "Japan" },
                    { "ke", "Kenya" },
                    { "kg", "Kyrgyzstan" },
                    { "kh", "Cambodia" },
                    { "ki", "Kiribati" },
                    { "km", "Comoros" },
                    { "kn", "Saint Kitts And Nevis" },
                    { "kp", "Korea, Democratic People'S Republic Of" },
                    { "kr", "Korea, Republic Of" },
                    { "kw", "Kuwait" },
                    { "ky", "Cayman Islands" },
                    { "kz", "Kazakhstan" },
                    { "la", "Lao People'S Democratic Republic" },
                    { "lb", "Lebanon" },
                    { "lc", "Saint Lucia" },
                    { "li", "Liechtenstein" },
                    { "lk", "Sri Lanka" },
                    { "lr", "Liberia" },
                    { "ls", "Lesotho" },
                    { "lt", "Lithuania" },
                    { "lu", "Luxembourg" },
                    { "lv", "Latvia" },
                    { "ly", "Libyan Arab Jamahiriya" },
                    { "ma", "Morocco" },
                    { "mc", "Monaco" },
                    { "md", "Moldova, Republic Of" },
                    { "mg", "Madagascar" },
                    { "mh", "Marshall Islands" },
                    { "mk", "Macedonia, The Former Yugoslav Republic Of" },
                    { "ml", "Mali" },
                    { "mm", "Myanmar" },
                    { "mn", "Mongolia" },
                    { "mo", "Macao" },
                    { "mp", "Northern Mariana Islands" },
                    { "mq", "Martinique" },
                    { "mr", "Mauritania" },
                    { "ms", "Montserrat" },
                    { "mt", "Malta" },
                    { "mu", "Mauritius" },
                    { "mv", "Maldives" },
                    { "mw", "Malawi" },
                    { "mx", "Mexico" },
                    { "my", "Malaysia" },
                    { "mz", "Mozambique" },
                    { "na", "Namibia" },
                    { "nc", "New Caledonia" },
                    { "ne", "Niger" },
                    { "nf", "Norfolk Island" },
                    { "ng", "Nigeria" },
                    { "ni", "Nicaragua" },
                    { "nl", "Netherlands" },
                    { "no", "Norway" },
                    { "np", "Nepal" },
                    { "nr", "Nauru" },
                    { "nu", "Niue" },
                    { "nz", "New Zealand" },
                    { "om", "Oman" },
                    { "pa", "Panama" },
                    { "pe", "Peru" },
                    { "pf", "French Polynesia" },
                    { "pg", "Papua New Guinea" },
                    { "ph", "Philippines" },
                    { "pk", "Pakistan" },
                    { "pl", "Poland" },
                    { "pm", "Saint Pierre And Miquelon" },
                    { "pn", "Pitcairn" },
                    { "pr", "Puerto Rico" },
                    { "ps", "Palestinian Territory, Occupied" },
                    { "pt", "Portugal" },
                    { "pw", "Palau" },
                    { "py", "Paraguay" },
                    { "qa", "Qatar" },
                    { "re", "Réunion" },
                    { "ro", "Romania" },
                    { "ru", "Russian Federation" },
                    { "rw", "Rwanda" },
                    { "sa", "Saudi Arabia" },
                    { "sb", "Solomon Islands" },
                    { "sc", "Seychelles" },
                    { "sd", "Sudan" },
                    { "se", "Sweden" },
                    { "sg", "Singapore" },
                    { "sh", "Saint Helena" },
                    { "si", "Slovenia" },
                    { "sj", "Svalbard And Jan Mayen" },
                    { "sk", "Slovakia" },
                    { "sl", "Sierra Leone" },
                    { "sm", "San Marino" },
                    { "sn", "Senegal" },
                    { "so", "Somalia" },
                    { "sr", "Suriname" },
                    { "st", "Sao Tome And Principe" },
                    { "sv", "El Salvador" },
                    { "sy", "Syrian Arab Republic" },
                    { "sz", "Swaziland" },
                    { "tc", "Turks And Caicos Islands" },
                    { "td", "Chad" },
                    { "tf", "French Southern Territories" },
                    { "tg", "Togo" },
                    { "th", "Thailand" },
                    { "tj", "Tajikistan" },
                    { "tk", "Tokelau" },
                    { "tl", "Timor-Leste" },
                    { "tm", "Turkmenistan" },
                    { "tn", "Tunisia" },
                    { "to", "Tonga" },
                    { "tr", "Turkey" },
                    { "tt", "Trinidad And Tobago" },
                    { "tv", "Tuvalu" },
                    { "tw", "Taiwan, Province Of China" },
                    { "tz", "Tanzania, United Republic Of" },
                    { "ua", "Ukraine" },
                    { "ug", "Uganda" },
                    { "um", "United States Minor Outlying Islands" },
                    { "us", "United States" },
                    { "uy", "Uruguay" },
                    { "uz", "Uzbekistan" },
                    { "va", "Holy See (Vatican City State)" },
                    { "vc", "Saint Vincent And The Grenadines" },
                    { "ve", "Venezuela" },
                    { "vg", "Virgin Islands, British" },
                    { "vi", "Virgin Islands, U.S." },
                    { "vn", "Viet Nam" },
                    { "vu", "Vanuatu" },
                    { "wf", "Wallis And Futuna" },
                    { "ws", "Samoa" },
                    { "ye", "Yemen" },
                    { "yt", "Mayotte" },
                    { "za", "South Africa" },
                    { "zm", "Zambia" },
                    { "zw", "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "ProvinceId", "Name" },
                values: new object[,]
                {
                    { 1, "An Giang" },
                    { 2, "Bà Rịa – Vũng Tàu" },
                    { 3, "Bắc Giang" },
                    { 4, "Bắc Kạn" },
                    { 5, "Bạc Liêu" },
                    { 6, "Bắc Ninh" },
                    { 7, "Bến Tre" },
                    { 8, "Bình Định" },
                    { 9, "Bình Dương" },
                    { 10, "Bình Phước" },
                    { 11, "Bình Thuận" },
                    { 12, "Cà Mau" },
                    { 13, "Cần Thơ" },
                    { 14, "Cao Bằng" },
                    { 15, "Đà Nẵng" },
                    { 16, "Đắk Lắk" },
                    { 17, "Đắk Nông" },
                    { 18, "Điện Biên" },
                    { 19, "Đồng Nai" },
                    { 20, "Đồng Tháp" },
                    { 21, "Gia Lai" },
                    { 22, "Hà Giang" },
                    { 23, "Hà Nam" },
                    { 24, "Hà Nội" },
                    { 25, "Hà Tĩnh" },
                    { 26, "Hải Dương" },
                    { 27, "Hải Phòng" },
                    { 28, "Hậu Giang" },
                    { 29, "Hòa Bình" },
                    { 30, "Hưng Yên" },
                    { 31, "Khánh Hòa" },
                    { 32, "Kiên Giang" },
                    { 33, "Kon Tum" },
                    { 34, "Lai Châu" },
                    { 35, "Lâm Đồng" },
                    { 36, "Lạng Sơn" },
                    { 37, "Lào Cai" },
                    { 38, "Long An" },
                    { 39, "Nam Định" },
                    { 40, "Nghệ An" },
                    { 41, "Ninh Bình" },
                    { 42, "Ninh Thuận" },
                    { 43, "Phú Thọ" },
                    { 44, "Phú Yên" },
                    { 45, "Quảng Bình" },
                    { 46, "Quảng Nam" },
                    { 47, "Quảng Ngãi" },
                    { 48, "Quảng Ninh" },
                    { 49, "Quảng Trị" },
                    { 50, "Sóc Trăng" },
                    { 51, "Sơn La" },
                    { 52, "Tây Ninh" },
                    { 53, "Thái Bình" },
                    { 54, "Thái Nguyên" },
                    { 55, "Thanh Hóa" },
                    { 56, "Thừa Thiên Huế" },
                    { 57, "Tiền Giang" },
                    { 58, "Thành phố Hồ Chí Minh" },
                    { 59, "Trà Vinh" },
                    { 60, "Tuyên Quang" },
                    { 61, "Vĩnh Long" },
                    { 62, "Vĩnh Phúc" },
                    { 63, "Yên Bái" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                column: "Type",
                value: "supplier");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewspaperImages_NewspaperId",
                table: "NewspaperImages",
                column: "NewspaperId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFavorites_ProductId",
                table: "ProductFavorites",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFavorites_UserId",
                table: "ProductFavorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_UserId",
                table: "ProductReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OriginId",
                table: "Products",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductViewHistories_ProductId",
                table: "ProductViewHistories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductViewHistories_UserId",
                table: "ProductViewHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContacts_SupplierId",
                table: "SupplierContacts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierReviews_SupplierId",
                table: "SupplierReviews",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierReviews_UserId",
                table: "SupplierReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ProvinceId",
                table: "Suppliers",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_UserId",
                table: "Suppliers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleType",
                table: "UserRoles",
                column: "RoleType");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FirebaseId",
                table: "Users",
                column: "FirebaseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "NewspaperImages");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductFavorites");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "ProductViewHistories");

            migrationBuilder.DropTable(
                name: "SupplierContacts");

            migrationBuilder.DropTable(
                name: "SupplierReviews");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Newspapers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Origin");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
