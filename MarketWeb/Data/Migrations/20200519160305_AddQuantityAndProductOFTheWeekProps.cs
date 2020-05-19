using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketWeb.Data.Migrations
{
    public partial class AddQuantityAndProductOFTheWeekProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProductOfTheWeek",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProductOfTheWeek",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");
        }
    }
}
