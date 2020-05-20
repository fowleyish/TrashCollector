using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f3f2d21-bc37-4eb2-b893-c01f80cbba95");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "242e3af3-3794-466c-b585-f2d8473759c9", "dfc8fb21-eade-431a-8098-3f48be7e0f1f", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85fd2943-7a8a-4204-8902-330b095b0f33", "f83bab63-7125-4344-a0fb-e9cdf45cdc27", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "242e3af3-3794-466c-b585-f2d8473759c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85fd2943-7a8a-4204-8902-330b095b0f33");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6f3f2d21-bc37-4eb2-b893-c01f80cbba95", "a70a4240-c612-44ac-8ddf-b17143014853", "Admin", "ADMIN" });
        }
    }
}
