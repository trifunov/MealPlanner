using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPlanner.Data.Migrations
{
    public partial class AlterActiveDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Meals_MealId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Plans_MealId_CompanyId_Shifts_ActiveFrom_ActiveTo",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "ActiveFrom",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ActiveTo",
                table: "Plans",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "MealId",
                table: "Orders",
                newName: "PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_MealId",
                table: "Orders",
                newName: "IX_Orders_PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_MealId_CompanyId_Shifts_Date",
                table: "Plans",
                columns: new[] { "MealId", "CompanyId", "Shifts", "Date" },
                unique: true,
                filter: "[Shifts] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Plans_PlanId",
                table: "Orders",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Plans_PlanId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Plans_MealId_CompanyId_Shifts_Date",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Plans",
                newName: "ActiveTo");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "Orders",
                newName: "MealId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PlanId",
                table: "Orders",
                newName: "IX_Orders_MealId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveFrom",
                table: "Plans",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Plans_MealId_CompanyId_Shifts_ActiveFrom_ActiveTo",
                table: "Plans",
                columns: new[] { "MealId", "CompanyId", "Shifts", "ActiveFrom", "ActiveTo" },
                unique: true,
                filter: "[Shifts] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Meals_MealId",
                table: "Orders",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
