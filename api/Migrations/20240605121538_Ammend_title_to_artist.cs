using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Ammend_title_to_artist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "194b210d-a8e9-45b7-8fe3-ca2611f2d0de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45494287-b56e-44aa-b53f-55fae63d9ae4");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Reviews",
                newName: "Artist");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b697eecf-a29e-4bbc-9bf4-2a59604cf396", null, "Admin", "ADMIN" },
                    { "d8041506-2d09-49e2-8408-8291fcd80f70", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b697eecf-a29e-4bbc-9bf4-2a59604cf396");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8041506-2d09-49e2-8408-8291fcd80f70");

            migrationBuilder.RenameColumn(
                name: "Artist",
                table: "Reviews",
                newName: "Title");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "194b210d-a8e9-45b7-8fe3-ca2611f2d0de", null, "User", "USER" },
                    { "45494287-b56e-44aa-b53f-55fae63d9ae4", null, "Admin", "ADMIN" }
                });
        }
    }
}
