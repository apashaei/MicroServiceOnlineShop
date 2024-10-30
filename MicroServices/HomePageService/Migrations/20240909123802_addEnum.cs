using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomPageServices.Migrations
{
    /// <inheritdoc />
    public partial class addEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "partsItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 9, 14, 38, 1, 964, DateTimeKind.Local).AddTicks(1220),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 9, 14, 32, 38, 814, DateTimeKind.Local).AddTicks(5563));

            migrationBuilder.AlterColumn<int>(
                name: "PartName",
                table: "homePageParts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "homePageParts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 9, 14, 38, 1, 964, DateTimeKind.Local).AddTicks(147),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 9, 14, 32, 38, 814, DateTimeKind.Local).AddTicks(4431));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "partsItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 9, 14, 32, 38, 814, DateTimeKind.Local).AddTicks(5563),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 9, 14, 38, 1, 964, DateTimeKind.Local).AddTicks(1220));

            migrationBuilder.AlterColumn<string>(
                name: "PartName",
                table: "homePageParts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "homePageParts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 9, 14, 32, 38, 814, DateTimeKind.Local).AddTicks(4431),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 9, 14, 38, 1, 964, DateTimeKind.Local).AddTicks(147));
        }
    }
}
