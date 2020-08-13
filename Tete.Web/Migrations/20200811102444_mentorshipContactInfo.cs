using Microsoft.EntityFrameworkCore.Migrations;

namespace Tete.Api.Migrations
{
    public partial class mentorshipContactInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LearnerContact",
                table: "Mentorships",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MentorContact",
                table: "Mentorships",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LearnerContact",
                table: "Mentorships");

            migrationBuilder.DropColumn(
                name: "MentorContact",
                table: "Mentorships");
        }
    }
}
