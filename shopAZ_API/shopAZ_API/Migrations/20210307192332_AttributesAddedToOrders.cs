using Microsoft.EntityFrameworkCore.Migrations;

namespace shopAZ_API.Migrations
{
    public partial class AttributesAddedToOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Discount",
                table: "ProductsOfOrders",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "IsMoney",
                table: "ProductsOfOrders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "ProductsOfOrders");

            migrationBuilder.DropColumn(
                name: "IsMoney",
                table: "ProductsOfOrders");
        }
    }
}
