using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CreatedBy_to_reviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ebd377b-2db2-45bd-8f8a-00787c627230");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3ee0476-2aec-4906-a73f-a4e6f7b32e10");

            migrationBuilder.AddColumn<string>(
                name: "UserAccountId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7d84a138-c379-4cfe-8d95-979d60903b81", null, "User", "USER" },
                    { "881127f9-9edb-4152-9686-818964a17bcd", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserAccountId",
                table: "Reviews",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserAccountId",
                table: "Reviews",
                column: "UserAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserAccountId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserAccountId",
                table: "Reviews");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d84a138-c379-4cfe-8d95-979d60903b81");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "881127f9-9edb-4152-9686-818964a17bcd");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Reviews");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ebd377b-2db2-45bd-8f8a-00787c627230", null, "User", "USER" },
                    { "e3ee0476-2aec-4906-a73f-a4e6f7b32e10", null, "Admin", "ADMIN" }
                });
        }
    }
}
