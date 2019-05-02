using Microsoft.EntityFrameworkCore.Migrations;

namespace Szertar.Dal.Migrations
{
    public partial class OrderItemUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderdItems_Orders_OrderId",
                table: "OrderdItems");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderdItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderdItems_Orders_OrderId",
                table: "OrderdItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderdItems_Orders_OrderId",
                table: "OrderdItems");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderdItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderdItems_Orders_OrderId",
                table: "OrderdItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
