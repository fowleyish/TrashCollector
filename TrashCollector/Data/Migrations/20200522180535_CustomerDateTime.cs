using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class CustomerDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e3d4e41-85b0-41c8-8e7e-399dbd3bf8be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8b0e33b-0f16-4500-88c7-835457a37bc6");

            migrationBuilder.AddColumn<DateTime>(
                name: "SpecialPickup",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SuspendEnd",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SuspendStart",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a277e146-f8d7-4d34-ade6-c41ddbd0c0a0", "00e4d347-29ba-408f-ab8d-b6a45850938f", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5c18ce72-64a1-474d-a558-234fe6df90ed", "e149647d-9840-4731-927e-359c2d6be082", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c18ce72-64a1-474d-a558-234fe6df90ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a277e146-f8d7-4d34-ade6-c41ddbd0c0a0");

            migrationBuilder.DropColumn(
                name: "SpecialPickup",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SuspendEnd",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SuspendStart",
                table: "Customers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a8b0e33b-0f16-4500-88c7-835457a37bc6", "4fe747fe-ab2d-47c8-b899-f4482151794c", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e3d4e41-85b0-41c8-8e7e-399dbd3bf8be", "52ab0528-8453-479c-9dbc-ad7889142210", "Employee", "EMPLOYEE" });
        }
    }
}
