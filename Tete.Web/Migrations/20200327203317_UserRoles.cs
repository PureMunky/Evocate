using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tete.Api.Migrations
{
    public partial class UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessRoles",
                columns: table => new
                {
                    AccessRoleId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRoles", x => x.AccessRoleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRoles");
        }
    }
}
