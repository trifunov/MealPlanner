using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPlanner.Data.Migrations
{
    public partial class Added_DeliveredDateSoftMeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredDate",
                table: "SoftMeals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveredDate",
                table: "SoftMeals");
        }
    }
}
