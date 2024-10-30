using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductServices.Migrations
{
    /// <inheritdoc />
    public partial class upadteCategory1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryComponents_CategoryComponents_CategoryId",
                table: "CategoryComponents");

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("07688566-d429-40de-85d7-90313352b65e"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("5cf56a7f-0623-4ab0-9764-9fb0f33a358b"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("f42dd3c7-e54f-408e-8b85-20a21bf6fbe0"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(1873),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(8548));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(2988),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 54, DateTimeKind.Local).AddTicks(31));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(1073),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(7306));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(204),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(6315));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CategoryComponents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 257, DateTimeKind.Local).AddTicks(9757),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(5934));

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId1",
                table: "CategoryComponents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 257, DateTimeKind.Local).AddTicks(5052),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(1518));

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "RemoveTime", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("83a69af4-fc7b-42c8-85a2-1dad1fdec0f8"), "محصولات Nike", "Nike", null, null },
                    { new Guid("8705da2c-a984-4bec-b5ab-88e9d218caa4"), "محصولات Apple", "Apple", null, null },
                    { new Guid("aa3d67d8-4283-4fc3-ba9e-9a5372620462"), "محصولات Sumsung", "Sumsung", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryComponents_CategoryId1",
                table: "CategoryComponents",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryComponents_CategoryComponents_CategoryId",
                table: "CategoryComponents",
                column: "CategoryId",
                principalTable: "CategoryComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryComponents_CategoryComponents_CategoryId1",
                table: "CategoryComponents",
                column: "CategoryId1",
                principalTable: "CategoryComponents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryComponents_CategoryComponents_CategoryId",
                table: "CategoryComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryComponents_CategoryComponents_CategoryId1",
                table: "CategoryComponents");

            migrationBuilder.DropIndex(
                name: "IX_CategoryComponents_CategoryId1",
                table: "CategoryComponents");

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

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "CategoryComponents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(8548),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(1873));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 54, DateTimeKind.Local).AddTicks(31),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(2988));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(7306),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(1073));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(6315),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 258, DateTimeKind.Local).AddTicks(204));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CategoryComponents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(5934),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 257, DateTimeKind.Local).AddTicks(9757));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(1518),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 20, 18, 34, 257, DateTimeKind.Local).AddTicks(5052));

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "RemoveTime", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("07688566-d429-40de-85d7-90313352b65e"), "محصولات Nike", "Nike", null, null },
                    { new Guid("5cf56a7f-0623-4ab0-9764-9fb0f33a358b"), "محصولات Sumsung", "Sumsung", null, null },
                    { new Guid("f42dd3c7-e54f-408e-8b85-20a21bf6fbe0"), "محصولات Apple", "Apple", null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryComponents_CategoryComponents_CategoryId",
                table: "CategoryComponents",
                column: "CategoryId",
                principalTable: "CategoryComponents",
                principalColumn: "Id");
        }
    }
}
