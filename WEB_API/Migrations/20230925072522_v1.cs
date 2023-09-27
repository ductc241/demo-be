using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_API.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Id = table.Column<int>(type: "int", nullable: false),
                    Customer_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Customer_Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Shipping_Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Total_Amount = table.Column<int>(type: "int", nullable: false),
                    Order_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payment_Method = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shipment_status",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipment_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shipping_carrier",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contact_Person = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone_Number = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping_carrier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tracking_status",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tracking_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "order_item",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Order_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_item_orders_Order_Id",
                        column: x => x.Order_Id,
                        principalSchema: "dbo",
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_item_products_Product_Id",
                        column: x => x.Product_Id,
                        principalSchema: "dbo",
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shipments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Customer_Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Customer_Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Shipping_Fee = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estimated_Delivery_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Actual_Delivery_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estimated_Arrival_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Actual_Arrival_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Order_Id = table.Column<int>(type: "int", nullable: false),
                    Shipment_Status_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shipments_orders_Order_Id",
                        column: x => x.Order_Id,
                        principalSchema: "dbo",
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shipments_shipment_status_Shipment_Status_Id",
                        column: x => x.Shipment_Status_Id,
                        principalSchema: "dbo",
                        principalTable: "shipment_status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shipment_detail",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shipping_Method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Driver_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Driver_Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Packaging_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Shipment_Id = table.Column<int>(type: "int", nullable: false),
                    Shipping_Carrier_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipment_detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shipment_detail_shipments_Shipment_Id",
                        column: x => x.Shipment_Id,
                        principalSchema: "dbo",
                        principalTable: "shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shipment_detail_shipping_carrier_Shipping_Carrier_Id",
                        column: x => x.Shipping_Carrier_Id,
                        principalSchema: "dbo",
                        principalTable: "shipping_carrier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tracking",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From_Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To_Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shipment_Id = table.Column<int>(type: "int", nullable: false),
                    Tracking_Status_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tracking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tracking_shipments_Shipment_Id",
                        column: x => x.Shipment_Id,
                        principalSchema: "dbo",
                        principalTable: "shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tracking_tracking_status_Tracking_Status_Id",
                        column: x => x.Tracking_Status_Id,
                        principalSchema: "dbo",
                        principalTable: "tracking_status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_item_Order_Id",
                schema: "dbo",
                table: "order_item",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_Product_Id",
                schema: "dbo",
                table: "order_item",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_shipment_detail_Shipment_Id",
                schema: "dbo",
                table: "shipment_detail",
                column: "Shipment_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shipment_detail_Shipping_Carrier_Id",
                schema: "dbo",
                table: "shipment_detail",
                column: "Shipping_Carrier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_shipments_Order_Id",
                schema: "dbo",
                table: "shipments",
                column: "Order_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shipments_Shipment_Status_Id",
                schema: "dbo",
                table: "shipments",
                column: "Shipment_Status_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tracking_Shipment_Id",
                schema: "dbo",
                table: "tracking",
                column: "Shipment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tracking_Tracking_Status_Id",
                schema: "dbo",
                table: "tracking",
                column: "Tracking_Status_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_item",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "shipment_detail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tracking",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "products",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "shipping_carrier",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "shipments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tracking_status",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "shipment_status",
                schema: "dbo");
        }
    }
}
