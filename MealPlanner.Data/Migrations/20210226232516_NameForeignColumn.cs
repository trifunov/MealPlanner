using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPlanner.Data.Migrations
{
    public partial class NameForeignColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainLanguage",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "IsMainLanguage",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "IsMainLanguage",
                table: "Allergens");

            migrationBuilder.AddColumn<string>(
                name: "NameForeign",
                table: "Meals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameForeign",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameForeign",
                table: "Allergens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameForeign",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "NameForeign",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "NameForeign",
                table: "Allergens");

            migrationBuilder.AddColumn<bool>(
                name: "IsMainLanguage",
                table: "Meals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainLanguage",
                table: "Ingredients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainLanguage",
                table: "Allergens",
                nullable: false,
                defaultValue: false);
        }
    }
}
