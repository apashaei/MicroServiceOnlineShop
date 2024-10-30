using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomPageServices.Migrations
{
    /// <inheritdoc />
    public partial class updateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HompePagePartsId",
                table: "HomePageParts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "HomePageParts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 9, 17, 54, 16, 330, DateTimeKind.Local).AddTicks(3315),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 9, 14, 58, 12, 752, DateTimeKind.Local).AddTicks(6179));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "HomePageParts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 9, 14, 58, 12, 752, DateTimeKind.Local).AddTicks(6179),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 9, 17, 54, 16, 330, DateTimeKind.Local).AddTicks(3315));

            migrationBuilder.AddColumn<int>(
                name: "HompePagePartsId",
                table: "HomePageParts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
