using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductServices.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

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

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "CategoryComponent");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryId",
                table: "CategoryComponent",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryId",
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
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(4915));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(6694),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(5930));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(4281),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(4190));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(3264),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(3486));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 892, DateTimeKind.Local).AddTicks(8134),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(1615));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CategoryComponent",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(1576),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(2553));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "CategoryComponent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "CategoryComponent");

            migrationBuilder.RenameTable(
                name: "CategoryComponent",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "ParentCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryComponent_CategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(4915),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(5266));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(5930),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(6694));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(4190),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(4281));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discount",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(3486),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(3264));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(1615),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 892, DateTimeKind.Local).AddTicks(8134));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 37, 48, 104, DateTimeKind.Local).AddTicks(2553),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 9, 22, 18, 16, 36, 893, DateTimeKind.Local).AddTicks(1576));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

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
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
