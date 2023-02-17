using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NerdyGadgets.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zipcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    image = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "CouponCodes",
                columns: table => new
                {
                    coupon = table.Column<string>(type: "uniqueidentifier", maxLength: 32, nullable: false),
                    percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    onetimeuse = table.Column<bool>(name: "one_time_use", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponCodes", x => x.coupon);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    preposition = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    lastname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    address = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    accountcreatedat = table.Column<DateTime>(name: "account_created_at", type: "datetime2", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_address",
                        column: x => x.address,
                        principalTable: "Addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productnumber = table.Column<int>(name: "product_number", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    category = table.Column<string>(type: "nvarchar(3)", nullable: true),
                    unitprice = table.Column<decimal>(name: "unit_price", type: "decimal(18,2)", nullable: false),
                    media = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.productnumber);
                    table.ForeignKey(
                        name: "FK_Products_Categories_category",
                        column: x => x.category,
                        principalTable: "Categories",
                        principalColumn: "code",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    orderedat = table.Column<DateTime>(name: "ordered_at", type: "datetime2", nullable: false),
                    expecteddeliverydate = table.Column<DateTime>(name: "expected_delivery_date", type: "datetime2", nullable: false),
                    comments = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    delivered = table.Column<bool>(type: "bit", nullable: false),
                    address = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    discountprice = table.Column<decimal>(name: "discount_price", type: "decimal(18,2)", nullable: true),
                    discountpercentage = table.Column<int>(name: "discount_percentage", type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_address",
                        column: x => x.address,
                        principalTable: "Addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_customer",
                        column: x => x.customer,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecs",
                columns: table => new
                {
                    product = table.Column<int>(type: "int", nullable: false),
                    specname = table.Column<string>(name: "spec_name", type: "nvarchar(128)", maxLength: 128, nullable: false),
                    specval = table.Column<string>(name: "spec_val", type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecs", x => new { x.product, x.specname });
                    table.ForeignKey(
                        name: "FK_ProductSpecs_Products_product",
                        column: x => x.product,
                        principalTable: "Products",
                        principalColumn: "product_number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    order = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => new { x.order, x.product });
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_order",
                        column: x => x.order,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderLines_Products_product",
                        column: x => x.product,
                        principalTable: "Products",
                        principalColumn: "product_number",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_code",
                table: "Categories",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_name",
                table: "Categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_product",
                table: "OrderLines",
                column: "product");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_address",
                table: "Orders",
                column: "address");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_customer",
                table: "Orders",
                column: "customer");

            migrationBuilder.CreateIndex(
                name: "IX_Products_category",
                table: "Products",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "IX_Products_name",
                table: "Products",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_address",
                table: "Users",
                column: "address");

            migrationBuilder.CreateIndex(
                name: "IX_Users_email",
                table: "Users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouponCodes");

            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "ProductSpecs");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Addresses");

        }
    }
}
