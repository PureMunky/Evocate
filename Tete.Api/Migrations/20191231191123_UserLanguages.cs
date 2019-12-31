using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tete.Api.Migrations
{
    public partial class UserLanguages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLanguages",
                columns: table => new
                {
                    UserLanguageId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Read = table.Column<bool>(nullable: false),
                    Speak = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLanguages", x => x.UserLanguageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLanguages");
        }
    }
}
