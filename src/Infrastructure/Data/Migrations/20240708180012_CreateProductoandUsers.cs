using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateProductoandUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "UserName", "UserType" },
                values: new object[,]
                {
                    { 1, "cueton@example.com", "Cueton912", "Cueton", "Client" },
                    { 2, "admin@example.com", "Admin123", "admin", "Admin" },
                    { 3, "miguel@example.com", "Miguelito3520", "Miguel", "Seller" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Name", "Price", "SellerId", "StockAvailable" },
                values: new object[,]
                {
                    { 1, "Mates", "MATE STANLEY", 35000m, 3, 6 },
                    { 2, "Termos", "TERMO LUMILAGRO", 20000m, 3, 1 },
                    { 3, "Materas", "MOCHILA MATERA DE CUERO", 25000m, 3, 5 },
                    { 4, "Bombillas", "BOMBILLA", 6500m, 3, 3 }
                });
        }

        /// <inheritdoc />
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

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
