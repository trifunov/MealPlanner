using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPlanner.Data.Migrations
{
    public partial class AddCompanyImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "4288333e-8764-4171-b329-7be08689ba9a", "9d424b57-aa0b-4114-b060-4b0021d46446" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "b9569794-e02a-4d90-b7cf-1f010790f8b1", "f3cfc2c2-87bb-4056-bcb1-9d2fea9571fb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f0e061c0-5329-4328-be24-11b91ed42f63", "723db442-5caf-4a90-a5fb-a4408437e336" });

            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Companies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4288333e-8764-4171-b329-7be08689ba9a", "9d424b57-aa0b-4114-b060-4b0021d46446", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b9569794-e02a-4d90-b7cf-1f010790f8b1", "f3cfc2c2-87bb-4056-bcb1-9d2fea9571fb", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f0e061c0-5329-4328-be24-11b91ed42f63", "723db442-5caf-4a90-a5fb-a4408437e336", "Chef", "CHEF" });
        }
    }
}
