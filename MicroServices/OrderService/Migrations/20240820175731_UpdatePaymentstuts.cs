using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderSevice.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentstuts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Paymetnstatus",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paymetnstatus",
                table: "Orders");
        }
    }
}
