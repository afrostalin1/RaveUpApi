using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AmmendNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3a38f70-7f18-4ab9-b436-da920dd31622");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7a3c7e7-431d-438b-ac97-27f997529831");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Reviews",
                newName: "ReviewBody");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "ReviewComments",
                newName: "CommentBody");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ebd377b-2db2-45bd-8f8a-00787c627230", null, "User", "USER" },
                    { "e3ee0476-2aec-4906-a73f-a4e6f7b32e10", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ebd377b-2db2-45bd-8f8a-00787c627230");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3ee0476-2aec-4906-a73f-a4e6f7b32e10");

            migrationBuilder.RenameColumn(
                name: "ReviewBody",
                table: "Reviews",
                newName: "Body");

            migrationBuilder.RenameColumn(
                name: "CommentBody",
                table: "ReviewComments",
                newName: "Body");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d3a38f70-7f18-4ab9-b436-da920dd31622", null, "Admin", "ADMIN" },
                    { "f7a3c7e7-431d-438b-ac97-27f997529831", null, "User", "USER" }
                });
        }
    }
}
