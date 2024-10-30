using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductServices.Migrations
{
    /// <inheritdoc />
    public partial class AddBrandSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 199, DateTimeKind.Local).AddTicks(1467),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 12, 47, 55, 492, DateTimeKind.Local).AddTicks(961));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 199, DateTimeKind.Local).AddTicks(2590),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 12, 47, 55, 492, DateTimeKind.Local).AddTicks(3078));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 199, DateTimeKind.Local).AddTicks(644),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 12, 47, 55, 491, DateTimeKind.Local).AddTicks(9441));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 198, DateTimeKind.Local).AddTicks(9823),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 12, 47, 55, 491, DateTimeKind.Local).AddTicks(7933));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 198, DateTimeKind.Local).AddTicks(8737),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 12, 47, 55, 491, DateTimeKind.Local).AddTicks(5825));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 198, DateTimeKind.Local).AddTicks(7605),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 12, 47, 55, 491, DateTimeKind.Local).AddTicks(3844));

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "RemoveTime", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("695cd902-78e6-497a-8377-d4868899f9c3"), "محصولات Nike", "Nike", null, null },
                    { new Guid("87ac5e5c-8538-4fee-bb1f-4f1ebf640df3"), "محصولات Apple", "Apple", null, null },
                    { new Guid("a7c2e478-b4cf-4c93-8947-0e00ff6aac48"), "محصولات Sumsung", "Sumsung", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("695cd902-78e6-497a-8377-d4868899f9c3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("87ac5e5c-8538-4fee-bb1f-4f1ebf640df3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("a7c2e478-b4cf-4c93-8947-0e00ff6aac48"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 12, 47, 55, 492, DateTimeKind.Local).AddTicks(961),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 199, DateTimeKind.Local).AddTicks(1467));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 12, 47, 55, 492, DateTimeKind.Local).AddTicks(3078),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 199, DateTimeKind.Local).AddTicks(2590));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 12, 47, 55, 491, DateTimeKind.Local).AddTicks(9441),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 199, DateTimeKind.Local).AddTicks(644));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 12, 47, 55, 491, DateTimeKind.Local).AddTicks(7933),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 198, DateTimeKind.Local).AddTicks(9823));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 12, 47, 55, 491, DateTimeKind.Local).AddTicks(5825),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 198, DateTimeKind.Local).AddTicks(8737));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 12, 47, 55, 491, DateTimeKind.Local).AddTicks(3844),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 198, DateTimeKind.Local).AddTicks(7605));
        }
    }
}
