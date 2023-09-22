using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FidelPoints.Migrations
{
    public partial class M04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrdersId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrdersId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrdersId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrdersId",
                table: "Products",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrdersId",
                table: "Products",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
