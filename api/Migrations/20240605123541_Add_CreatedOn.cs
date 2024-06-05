using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Add_CreatedOn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b697eecf-a29e-4bbc-9bf4-2a59604cf396");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8041506-2d09-49e2-8408-8291fcd80f70");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ReviewComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d3a38f70-7f18-4ab9-b436-da920dd31622", null, "Admin", "ADMIN" },
                    { "f7a3c7e7-431d-438b-ac97-27f997529831", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3a38f70-7f18-4ab9-b436-da920dd31622");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7a3c7e7-431d-438b-ac97-27f997529831");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ReviewComments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b697eecf-a29e-4bbc-9bf4-2a59604cf396", null, "Admin", "ADMIN" },
                    { "d8041506-2d09-49e2-8408-8291fcd80f70", null, "User", "USER" }
                });
        }
    }
}
