using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lattency.Migrations
{
    /// <inheritdoc />
    public partial class SeedMenuandDishItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "Dishes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Drinks" },
                    { 5, "Pastries" }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "Description", "DishName", "ImageURL", "IsPopular", "MenuId", "Price" },
                values: new object[,]
                {
                    { 1, "Rich and bold single shot of espresso", "Espresso", null, true, 4, 25m },
                    { 2, "Espresso with steamed milk and foam", "Cappuccino", null, false, 4, 39m },
                    { 3, "Smooth espresso with lots of steamed milk", "Latte", null, true, 4, 42m },
                    { 4, "Cold espresso with milk over ice", "Iced Latte", null, false, 4, 45m },
                    { 5, "Creamy green tea latte", "Matcha Latte", null, false, 4, 49m },
                    { 6, "Classic cocoa with whipped cream", "Hot Chocolate", null, false, 4, 35m },
                    { 7, "Spiced tea latte with cinnamon and cardamom", "Chai Latte", null, false, 4, 44m },
                    { 8, "Flaky butter croissant", "Croissant", null, false, 5, 29m },
                    { 9, "Buttery pastry filled with chocolate", "Pain au Chocolat", null, false, 5, 34m },
                    { 10, "Sweet bun with cinnamon filling", "Cinnamon Bun", null, true, 5, 32m },
                    { 11, "Soft muffin with fresh blueberries", "Blueberry Muffin", null, false, 5, 36m },
                    { 12, "Moist cake with cream cheese frosting", "Carrot Cake", null, false, 5, 45m },
                    { 13, "Creamy cheesecake with berry topping", "Cheesecake", null, false, 5, 55m },
                    { 14, "Classic cheesecake topped with fresh strawberries", "Strawberry Cheesecake", null, true, 5, 59m },
                    { 15, "Sweet and creamy milk tea with brown sugar pearls", "Brown Sugar Boba Tea", null, true, 4, 55m },
                    { 16, "Refreshing strawberry milk tea with chewy tapioca pearls", "Strawberry Boba Tea", null, false, 4, 55m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "Dishes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
