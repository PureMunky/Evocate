using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tete.Api.Migrations
{
  public partial class MentorshipClosingData : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "LearnerClosingComments",
          table: "Mentorships");

      migrationBuilder.DropColumn(
          name: "LearnerRating",
          table: "Mentorships");

      migrationBuilder.DropColumn(
          name: "MentorClosingComments",
          table: "Mentorships");

      migrationBuilder.DropColumn(
          name: "MentorRating",
          table: "Mentorships");

      migrationBuilder.CreateTable(
          name: "Evaluations",
          columns: table => new
          {
            EvaluationId = table.Column<Guid>(nullable: false),
            MentorshipId = table.Column<Guid>(nullable: false),
            UserId = table.Column<Guid>(nullable: false),
            CreatedDate = table.Column<DateTime>(nullable: false),
            Rating = table.Column<int>(nullable: false),
            Comments = table.Column<string>(nullable: true),
            UserType = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Evaluations", x => x.EvaluationId);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Evaluations");

      migrationBuilder.AddColumn<string>(
          name: "LearnerClosingComments",
          table: "Mentorships",
          nullable: true);

      migrationBuilder.AddColumn<int>(
          name: "LearnerRating",
          table: "Mentorships",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<string>(
          name: "MentorClosingComments",
          table: "Mentorships",
          nullable: true);

      migrationBuilder.AddColumn<int>(
          name: "MentorRating",
          table: "Mentorships",
          nullable: false,
          defaultValue: 0);
    }
  }
}
