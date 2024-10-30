using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductServices.Migrations
{
    /// <inheritdoc />
    public partial class updateTabels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("83a69af4-fc7b-42c8-85a2-1dad1fdec0f8"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("8705da2c-a984-4bec-b5ab-88e9d218caa4"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("aa3d67d8-4283-4fc3-ba9e-9a5372620462"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(5569),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(1873));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(6593),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(2988));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(4899),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(1073));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(4213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(204));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CategoryComponents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(3993),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 257, DateTimeKind.Local).AddTicks(9757));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(1272),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 257, DateTimeKind.Local).AddTicks(5052));

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "RemoveTime", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("4dd204b0-9b54-4a28-b93d-5bb5bcf05ac4"), "محصولات Nike", "Nike", null, null },
                    { new Guid("87d31a14-0c7e-44c6-9f24-0236aa250337"), "محصولات Apple", "Apple", null, null },
                    { new Guid("dd6d7cab-9785-4ec3-9962-14bd77d5946e"), "محصولات Sumsung", "Sumsung", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("4dd204b0-9b54-4a28-b93d-5bb5bcf05ac4"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("87d31a14-0c7e-44c6-9f24-0236aa250337"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("dd6d7cab-9785-4ec3-9962-14bd77d5946e"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(1873),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(5569));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(2988),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(6593));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(1073),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(4899));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(204),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(4213));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CategoryComponents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 257, DateTimeKind.Local).AddTicks(9757),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(3993));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 257, DateTimeKind.Local).AddTicks(5052),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(1272));

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "RemoveTime", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("83a69af4-fc7b-42c8-85a2-1dad1fdec0f8"), "محصولات Nike", "Nike", null, null },
                    { new Guid("8705da2c-a984-4bec-b5ab-88e9d218caa4"), "محصولات Apple", "Apple", null, null },
                    { new Guid("aa3d67d8-4283-4fc3-ba9e-9a5372620462"), "محصولات Sumsung", "Sumsung", null, null }
                });
        }
    }
}
