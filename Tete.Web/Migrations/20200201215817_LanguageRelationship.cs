using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tete.Api.Migrations
{
    public partial class LanguageRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    ElementId = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    LanguageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.ElementId);
                    table.ForeignKey(
                        name: "FK_Elements_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Elements_LanguageId",
                table: "Elements",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Elements");
        }
    }
}
