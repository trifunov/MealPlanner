using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPlanner.Data.Migrations
{
    public partial class ShiftsAlter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Plans_MealId_CompanyId_Shift_ActiveFrom_ActiveTo",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "Shift",
                table: "Plans");

            migrationBuilder.AddColumn<string>(
                name: "Shifts",
                table: "Plans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_MealId_CompanyId_Shifts_ActiveFrom_ActiveTo",
                table: "Plans",
                columns: new[] { "MealId", "CompanyId", "Shifts", "ActiveFrom", "ActiveTo" },
                unique: true,
                filter: "[Shifts] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Plans_MealId_CompanyId_Shifts_ActiveFrom_ActiveTo",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "Shifts",
                table: "Plans");

            migrationBuilder.AddColumn<int>(
                name: "Shift",
                table: "Plans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_MealId_CompanyId_Shift_ActiveFrom_ActiveTo",
                table: "Plans",
                columns: new[] { "MealId", "CompanyId", "Shift", "ActiveFrom", "ActiveTo" },
                unique: true);
        }
    }
}
