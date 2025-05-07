using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhoneBookWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerToContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUsername",
                table: "Contacts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Category", "Name", "OwnerUsername", "Phone" },
                values: new object[,]
                {
                    { 1, "Семья", "Иванов Иван", "user1", "+7 900 111-22-33" },
                    { 2, "Работа", "Петров Пётр", "admin1", "+7 901 222-33-44" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "OwnerUsername",
                table: "Contacts");
        }
    }
}
