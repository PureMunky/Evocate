using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tete.Api.Migrations
{
    public partial class Profileaboutfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    ProfileId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    About = table.Column<string>(nullable: true),
                    PrivateAbout = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.ProfileId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
