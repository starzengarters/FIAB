using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FIAB.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class InitializeRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "97646990-de1f-43eb-b697-c76726827de8", "51ba2359-0718-4d62-be67-20fdc1d680d7", "User", "USER" },
                    { "eb05a14f-c339-472f-9479-86a03cfcf4dc", "fc39d6be-6f26-45ed-a3ef-3d55ad06f82e", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97646990-de1f-43eb-b697-c76726827de8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb05a14f-c339-472f-9479-86a03cfcf4dc");
        }
    }
}
