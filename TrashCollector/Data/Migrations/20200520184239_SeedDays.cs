using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class SeedDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "242e3af3-3794-466c-b585-f2d8473759c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85fd2943-7a8a-4204-8902-330b095b0f33");

            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    DayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.DayId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "de8e8790-8897-448c-8a0c-cbdba56ad2d0", "ca80c8c4-8e29-4946-a6cc-e464ee15dc3a", "Customer", "CUSTOMER" },
                    { "7f11e149-d712-41d7-abce-55e1f39be61f", "4d35282f-27b5-4c7e-999c-ba76401fdea4", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "Day",
                columns: new[] { "DayId", "DayOfWeek" },
                values: new object[,]
                {
                    { 1, "Sunday" },
                    { 2, "Monday" },
                    { 3, "Tuesday" },
                    { 4, "Wednesday" },
                    { 5, "Thursday" },
                    { 6, "Friday" },
                    { 7, "Saturday" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f11e149-d712-41d7-abce-55e1f39be61f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de8e8790-8897-448c-8a0c-cbdba56ad2d0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "242e3af3-3794-466c-b585-f2d8473759c9", "dfc8fb21-eade-431a-8098-3f48be7e0f1f", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85fd2943-7a8a-4204-8902-330b095b0f33", "f83bab63-7125-4344-a0fb-e9cdf45cdc27", "Employee", "EMPLOYEE" });
        }
    }
}
