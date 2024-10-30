using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductServices.Migrations
{
    /// <inheritdoc />
    public partial class allLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                defaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(7591),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(5569));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(8733),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(6593));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(6739),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(4899));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(5926),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(4213));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CategoryComponents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(5670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(3993));

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "CategoryComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(2288),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(1272));

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "RemoveTime", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("05904e6e-136a-46d4-a56d-2217f1a28b9e"), "محصولات Apple", "Apple", null, null },
                    { new Guid("3e2837e7-9b69-4079-a393-ab93095ec395"), "محصولات Sumsung", "Sumsung", null, null },
                    { new Guid("b0017758-1af3-4403-8a06-72f08ca217b6"), "محصولات Nike", "Nike", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("05904e6e-136a-46d4-a56d-2217f1a28b9e"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("3e2837e7-9b69-4079-a393-ab93095ec395"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b0017758-1af3-4403-8a06-72f08ca217b6"));

            migrationBuilder.DropColumn(
                name: "Link",
                table: "CategoryComponents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(5569),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(7591));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(6593),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(8733));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(4899),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(6739));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(4213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(5926));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CategoryComponents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(3993),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(5670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 20, 13, 151, DateTimeKind.Local).AddTicks(1272),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(2288));

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
    }
}
