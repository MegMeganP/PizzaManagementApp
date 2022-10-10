using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaApplication.Migrations
{
    public partial class pizzatoppings2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ToppingId",
                table: "PizzaTopping",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PizzaTopping_ToppingId",
                table: "PizzaTopping",
                column: "ToppingId");

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaTopping_Toppings_ToppingId",
                table: "PizzaTopping",
                column: "ToppingId",
                principalTable: "Toppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PizzaTopping_Toppings_ToppingId",
                table: "PizzaTopping");

            migrationBuilder.DropIndex(
                name: "IX_PizzaTopping_ToppingId",
                table: "PizzaTopping");

            migrationBuilder.DropColumn(
                name: "ToppingId",
                table: "PizzaTopping");
        }
    }
}
