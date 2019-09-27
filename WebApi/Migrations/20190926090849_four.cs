using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_wheatherFort",
                table: "wheatherFort");

            migrationBuilder.RenameTable(
                name: "wheatherFort",
                newName: "Wheather");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wheather",
                table: "Wheather",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Wheather",
                table: "Wheather");

            migrationBuilder.RenameTable(
                name: "Wheather",
                newName: "wheatherFort");

            migrationBuilder.AddPrimaryKey(
                name: "PK_wheatherFort",
                table: "wheatherFort",
                column: "Id");
        }
    }
}
