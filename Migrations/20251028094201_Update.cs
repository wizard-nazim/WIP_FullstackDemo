using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryApi.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "nazim@milkyway.com", "$2b$10$yRcac5XzNqWBrKZLY3pr2uD5IQHLc43OydI5P1HL0vuFSWf6iJHkK" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "PasswordHash" },
                values: new object[] { "admin@example.com", "$2a$11$OY7PiVxEqMI7OtEm5v2u0eCPu/sPI3AcxngEInxG3OaSsvTPZXIMy" });
        }
    }
}
