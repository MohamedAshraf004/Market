using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketWeb.Data.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "IsProductOfTheWeek", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, "Awesome Laptop!", "HP.PNG", true, "HP ProBook", 152.94999999999999, 6 },
                    { 2, 1, "Awesome Laptop!", "Mac.JPG", true, "Mac Book", 252.94999999999999, 6 },
                    { 3, 3, "Awesome Phone!", "Phone.JPG", true, "IPhone11 Pro", 175.94999999999999, 3 },
                    { 4, 2, "Awesome TV!", "TV.JPG", true, "Mac Tv", 202.94999999999999, 6 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
