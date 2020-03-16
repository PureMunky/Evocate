using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tete.Api.Migrations
{
    public partial class UpdatedUserLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "UserLanguages",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguages_LanguageId",
                table: "UserLanguages",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguages_Languages_LanguageId",
                table: "UserLanguages",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguages_Languages_LanguageId",
                table: "UserLanguages");

            migrationBuilder.DropIndex(
                name: "IX_UserLanguages_LanguageId",
                table: "UserLanguages");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "UserLanguages");
        }
    }
}
