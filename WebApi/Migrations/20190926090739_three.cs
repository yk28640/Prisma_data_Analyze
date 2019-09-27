using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class three : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_wheatherForecast",
                table: "wheatherForecast");

            migrationBuilder.RenameTable(
                name: "wheatherForecast",
                newName: "wheatherFort");

            migrationBuilder.AddPrimaryKey(
                name: "PK_wheatherFort",
                table: "wheatherFort",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_wheatherFort",
                table: "wheatherFort");

            migrationBuilder.RenameTable(
                name: "wheatherFort",
                newName: "wheatherForecast");

            migrationBuilder.AddPrimaryKey(
                name: "PK_wheatherForecast",
                table: "wheatherForecast",
                column: "Id");
        }
    }
}
