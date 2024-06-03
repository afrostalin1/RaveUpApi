using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CommentUserAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ca3b722-2c76-48c1-ae8c-1975e58df134");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7985471-f811-4dcd-acfd-aaf9919e4e48");

            migrationBuilder.AddColumn<string>(
                name: "UserAccountId",
                table: "ReviewComments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "194b210d-a8e9-45b7-8fe3-ca2611f2d0de", null, "User", "USER" },
                    { "45494287-b56e-44aa-b53f-55fae63d9ae4", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_UserAccountId",
                table: "ReviewComments",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComments_AspNetUsers_UserAccountId",
                table: "ReviewComments",
                column: "UserAccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComments_AspNetUsers_UserAccountId",
                table: "ReviewComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_UserAccountId",
                table: "ReviewComments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "194b210d-a8e9-45b7-8fe3-ca2611f2d0de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45494287-b56e-44aa-b53f-55fae63d9ae4");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "ReviewComments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ca3b722-2c76-48c1-ae8c-1975e58df134", null, "Admin", "ADMIN" },
                    { "a7985471-f811-4dcd-acfd-aaf9919e4e48", null, "User", "USER" }
                });
        }
    }
}
