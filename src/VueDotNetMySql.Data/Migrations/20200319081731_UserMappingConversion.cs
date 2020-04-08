using Microsoft.EntityFrameworkCore.Migrations;

namespace VueDotNetMySql.Data.Migrations
{
    public partial class UserMappingConversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "IsActive",
                table: "Users",
                nullable: false,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "IsActive",
                table: "Users",
                nullable: false,
                oldClrType: typeof(short));
        }
    }
}
