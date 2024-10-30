using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomPageServices.Migrations
{
    /// <inheritdoc />
    public partial class deleteHomePage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partsItem_homePageParts_HomePagePartsId",
                table: "partsItem");

            migrationBuilder.DropTable(
                name: "homePageParts");

            migrationBuilder.DropIndex(
                name: "IX_partsItem_HomePagePartsId",
                table: "partsItem");

            migrationBuilder.RenameColumn(
                name: "HomePagePartsId",
                table: "partsItem",
                newName: "Part");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "partsItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 9, 14, 56, 27, 121, DateTimeKind.Local).AddTicks(2134),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 9, 14, 38, 1, 964, DateTimeKind.Local).AddTicks(1220));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Part",
                table: "partsItem",
                newName: "HomePagePartsId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "partsItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 9, 14, 38, 1, 964, DateTimeKind.Local).AddTicks(1220),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 9, 14, 56, 27, 121, DateTimeKind.Local).AddTicks(2134));

            migrationBuilder.CreateTable(
                name: "homePageParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 9, 9, 14, 38, 1, 964, DateTimeKind.Local).AddTicks(147)),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PartName = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_homePageParts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_partsItem_HomePagePartsId",
                table: "partsItem",
                column: "HomePagePartsId");

            migrationBuilder.AddForeignKey(
                name: "FK_partsItem_homePageParts_HomePagePartsId",
                table: "partsItem",
                column: "HomePagePartsId",
                principalTable: "homePageParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
