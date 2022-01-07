using Microsoft.EntityFrameworkCore.Migrations;

namespace API_MVC_Discriminator.Persistence.Migrations
{
    public partial class AddSeldDataRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "e8206942-dace-4b64-b9fb-2af020b5fa79", "Admin", "ADMIN" },
                    { "2", "d5ffcb06-4e40-4f34-9f34-6a04b190b6e1", "Moderator", "MODERATOR" },
                    { "3", "64fc640e-3507-4435-9dd3-3092088549ee", "Member", "MEMBER" },
                    { "4", "6a4aa1e0-38fe-4e59-ac37-5186407cd596", "VIP", "VIP" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4");
        }
    }
}
