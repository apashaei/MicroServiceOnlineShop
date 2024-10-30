using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductServices.Migrations
{
    /// <inheritdoc />
    public partial class updateProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 18, 17, 10, 38, 307, DateTimeKind.Local).AddTicks(4546),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(7591));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 18, 17, 10, 38, 307, DateTimeKind.Local).AddTicks(6710),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(8733));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 18, 17, 10, 38, 307, DateTimeKind.Local).AddTicks(1600),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(6739));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 18, 17, 10, 38, 306, DateTimeKind.Local).AddTicks(9431),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(5926));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CategoryComponents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 18, 17, 10, 38, 306, DateTimeKind.Local).AddTicks(8620),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(5670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 18, 17, 10, 38, 306, DateTimeKind.Local).AddTicks(2628),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(2288));

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "RemoveTime", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("16833715-547b-4497-a0a8-1c4700410cc7"), "محصولات Nike", "Nike", null, null },
                    { new Guid("3b399a9d-60c4-4582-bf9d-e8b9bd0af77b"), "محصولات Apple", "Apple", null, null },
                    { new Guid("71654cdf-c97f-424c-b4a8-5fc42d975a0f"), "محصولات Sumsung", "Sumsung", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("16833715-547b-4497-a0a8-1c4700410cc7"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("3b399a9d-60c4-4582-bf9d-e8b9bd0af77b"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("71654cdf-c97f-424c-b4a8-5fc42d975a0f"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(7591),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 18, 17, 10, 38, 307, DateTimeKind.Local).AddTicks(4546));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(8733),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 18, 17, 10, 38, 307, DateTimeKind.Local).AddTicks(6710));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(6739),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 18, 17, 10, 38, 307, DateTimeKind.Local).AddTicks(1600));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(5926),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 18, 17, 10, 38, 306, DateTimeKind.Local).AddTicks(9431));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CategoryComponents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(5670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 18, 17, 10, 38, 306, DateTimeKind.Local).AddTicks(8620));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 26, 12, 29, 45, 813, DateTimeKind.Local).AddTicks(2288),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 18, 17, 10, 38, 306, DateTimeKind.Local).AddTicks(2628));

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
    }
}
