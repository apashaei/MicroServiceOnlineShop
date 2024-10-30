using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductServices.Migrations
{
    /// <inheritdoc />
    public partial class updateDiscountField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discount_DiscountId",
                table: "Products");

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
                defaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(4915),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 199, DateTimeKind.Local).AddTicks(1467));

            migrationBuilder.AlterColumn<Guid>(
                name: "DiscountId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(5930),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 199, DateTimeKind.Local).AddTicks(2590));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(4190),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 199, DateTimeKind.Local).AddTicks(644));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(3486),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 198, DateTimeKind.Local).AddTicks(9823));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(2553),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 198, DateTimeKind.Local).AddTicks(8737));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(1615),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 198, DateTimeKind.Local).AddTicks(7605));

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "RemoveTime", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("477c6ef9-7cfa-4782-8d9c-3945e28ee8da"), "محصولات Nike", "Nike", null, null },
                    { new Guid("e0ccfb84-8ee9-49bc-880e-6f39f6e2e1f3"), "محصولات Apple", "Apple", null, null },
                    { new Guid("f4c59d5b-a9b7-47c4-bc5c-2b8d5477dae2"), "محصولات Sumsung", "Sumsung", null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discount_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discount_DiscountId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("477c6ef9-7cfa-4782-8d9c-3945e28ee8da"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e0ccfb84-8ee9-49bc-880e-6f39f6e2e1f3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("f4c59d5b-a9b7-47c4-bc5c-2b8d5477dae2"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 199, DateTimeKind.Local).AddTicks(1467),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(4915));

            migrationBuilder.AlterColumn<Guid>(
                name: "DiscountId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 199, DateTimeKind.Local).AddTicks(2590),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(5930));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 199, DateTimeKind.Local).AddTicks(644),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(4190));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 198, DateTimeKind.Local).AddTicks(9823),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(3486));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 198, DateTimeKind.Local).AddTicks(8737),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(2553));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 15, 38, 40, 198, DateTimeKind.Local).AddTicks(7605),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(1615));

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "RemoveTime", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("695cd902-78e6-497a-8377-d4868899f9c3"), "محصولات Nike", "Nike", null, null },
                    { new Guid("87ac5e5c-8538-4fee-bb1f-4f1ebf640df3"), "محصولات Apple", "Apple", null, null },
                    { new Guid("a7c2e478-b4cf-4c93-8947-0e00ff6aac48"), "محصولات Sumsung", "Sumsung", null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discount_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
