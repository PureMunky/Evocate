using Microsoft.EntityFrameworkCore.Migrations;

namespace Tete.Api.Migrations
{
  public partial class UserName : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "UserName",
          table: "Users",
          nullable: false,
          defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "UserName",
          table: "Users");
    }
  }
}
