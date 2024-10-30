using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductServices.Migrations
{
    /// <inheritdoc />
    public partial class upadteCategory2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryComponent_CategoryComponent_CategoryId",
                table: "CategoryComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoryComponent_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryComponent",
                table: "CategoryComponent");

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("62e03e65-f123-423a-a27e-ec9f5c7e26e6"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("a7d88b1b-a9ab-4a29-83cb-62224618cbe5"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("db2f7387-8440-4c26-bf62-ca10b0d38642"));

            migrationBuilder.RenameTable(
                name: "CategoryComponent",
                newName: "CategoryComponents");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryComponent_CategoryId",
                table: "CategoryComponents",
                newName: "IX_CategoryComponents_CategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(8548),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(5266));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 54, DateTimeKind.Local).AddTicks(31),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(6694));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(7306),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(4281));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(6315),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(3264));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(1518),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 892, DateTimeKind.Local).AddTicks(8134));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CategoryComponents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(5934),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(1576));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryComponents",
                table: "CategoryComponents",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoryComponents_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "CategoryComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryComponents_CategoryComponents_CategoryId",
                table: "CategoryComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoryComponents_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryComponents",
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

            migrationBuilder.RenameTable(
                name: "CategoryComponents",
                newName: "CategoryComponent");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryComponents_CategoryId",
                table: "CategoryComponent",
                newName: "IX_CategoryComponent_CategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(5266),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(8548));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(6694),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 54, DateTimeKind.Local).AddTicks(31));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(4281),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(7306));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(3264),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(6315));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 892, DateTimeKind.Local).AddTicks(8134),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(1518));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CategoryComponent",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(1576),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 38, 42, 53, DateTimeKind.Local).AddTicks(5934));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryComponent",
                table: "CategoryComponent",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Description", "Name", "RemoveTime", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("62e03e65-f123-423a-a27e-ec9f5c7e26e6"), "محصولات Nike", "Nike", null, null },
                    { new Guid("a7d88b1b-a9ab-4a29-83cb-62224618cbe5"), "محصولات Sumsung", "Sumsung", null, null },
                    { new Guid("db2f7387-8440-4c26-bf62-ca10b0d38642"), "محصولات Apple", "Apple", null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryComponent_CategoryComponent_CategoryId",
                table: "CategoryComponent",
                column: "CategoryId",
                principalTable: "CategoryComponent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CategoryComponent_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "CategoryComponent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
