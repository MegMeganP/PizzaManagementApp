using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaApplication.Migrations
{
    public partial class toppingunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ToppingName",
                table: "Toppings",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Toppings_ToppingName",
                table: "Toppings",
                column: "ToppingName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Toppings_ToppingName",
                table: "Toppings");

            migrationBuilder.AlterColumn<string>(
                name: "ToppingName",
                table: "Toppings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
