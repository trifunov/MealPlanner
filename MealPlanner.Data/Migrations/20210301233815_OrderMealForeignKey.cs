using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPlanner.Data.Migrations
{
    public partial class OrderMealForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Plans_PlanId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "Orders",
                newName: "MealId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PlanId",
                table: "Orders",
                newName: "IX_Orders_MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Meals_MealId",
                table: "Orders",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Meals_MealId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "MealId",
                table: "Orders",
                newName: "PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_MealId",
                table: "Orders",
                newName: "IX_Orders_PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Plans_PlanId",
                table: "Orders",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
