using Microsoft.EntityFrameworkCore.Migrations;

namespace MealPlanner.Data.Migrations
{
    public partial class AddedHRRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    //{ "e13f6db0-58f0-4bf8-9053-f33f8cba8cdf", "149568d0-8848-4c9a-a275-9d29eb50eaae", "Manager", "MANAGER" },
                    //{ "a7261f15-b1c6-4e44-822e-15e400f85ef3", "c7c54efd-17b6-49ae-b362-df8efc1ce82b", "Administrator", "ADMINISTRATOR" },
                    //{ "a03d0ccd-d990-4321-bb49-438b39cf5014", "828e53d2-23b2-47d3-ad24-c9baf99adb96", "Chef", "CHEF" },
                    { "774875ed-bf64-4b35-8642-fb437da157a6", "120ddc3f-5d0b-4085-b84b-6f67c7aa1c2b", "HR", "HR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "774875ed-bf64-4b35-8642-fb437da157a6", "120ddc3f-5d0b-4085-b84b-6f67c7aa1c2b" });

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumns: new[] { "Id", "ConcurrencyStamp" },
            //    keyValues: new object[] { "a03d0ccd-d990-4321-bb49-438b39cf5014", "828e53d2-23b2-47d3-ad24-c9baf99adb96" });

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumns: new[] { "Id", "ConcurrencyStamp" },
            //    keyValues: new object[] { "a7261f15-b1c6-4e44-822e-15e400f85ef3", "c7c54efd-17b6-49ae-b362-df8efc1ce82b" });

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumns: new[] { "Id", "ConcurrencyStamp" },
            //    keyValues: new object[] { "e13f6db0-58f0-4bf8-9053-f33f8cba8cdf", "149568d0-8848-4c9a-a275-9d29eb50eaae" });
        }
    }
}
