using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketServices.Migrations
{
    /// <inheritdoc />
    public partial class addProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Unitprice",
                table: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Unitprice",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
