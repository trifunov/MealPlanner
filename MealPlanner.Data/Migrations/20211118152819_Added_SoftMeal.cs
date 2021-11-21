using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPlanner.Data.Migrations
{
    public partial class Added_SoftMeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    SoftMealId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoftMealDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageBase64 = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NameForeign = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftMealDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoftMeals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDelivered = table.Column<bool>(nullable: false),
                    Shift = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    SoftMealDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftMeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoftMeals_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoftMeals_SoftMealDetails_SoftMealDetailId",
                        column: x => x.SoftMealDetailId,
                        principalTable: "SoftMealDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryLogs_EmployeeId",
                table: "DeliveryLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftMeals_EmployeeId",
                table: "SoftMeals",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftMeals_SoftMealDetailId",
                table: "SoftMeals",
                column: "SoftMealDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryLogs");

            migrationBuilder.DropTable(
                name: "SoftMeals");

            migrationBuilder.DropTable(
                name: "SoftMealDetails");
        }
    }
}
