using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomPageServices.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "partsItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 9, 14, 32, 38, 814, DateTimeKind.Local).AddTicks(5563));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "partsItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "partsItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "partsItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "homePageParts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 9, 14, 32, 38, 814, DateTimeKind.Local).AddTicks(4431));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "homePageParts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "homePageParts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "homePageParts",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "partsItem");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "partsItem");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "partsItem");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "partsItem");

            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "homePageParts");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "homePageParts");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "homePageParts");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "homePageParts");
        }
    }
}
