using Microsoft.EntityFrameworkCore.Migrations;

namespace Tete.Api.Migrations
{
    public partial class LoggingDataAndDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "Logs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Domain",
                table: "Logs");
        }
    }
}
