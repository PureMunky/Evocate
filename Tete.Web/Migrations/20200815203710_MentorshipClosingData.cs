using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tete.Api.Migrations
{
    public partial class MentorshipClosingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LearnerClosed",
                table: "Mentorships",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LearnerClosedDate",
                table: "Mentorships",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LearnerClosingComments",
                table: "Mentorships",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LearnerRating",
                table: "Mentorships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "MentorClosed",
                table: "Mentorships",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "MentorClosedDate",
                table: "Mentorships",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LearnerClosed",
                table: "Mentorships");

            migrationBuilder.DropColumn(
                name: "LearnerClosedDate",
                table: "Mentorships");

            migrationBuilder.DropColumn(
                name: "LearnerClosingComments",
                table: "Mentorships");

            migrationBuilder.DropColumn(
                name: "LearnerRating",
                table: "Mentorships");

            migrationBuilder.DropColumn(
                name: "MentorClosed",
                table: "Mentorships");

            migrationBuilder.DropColumn(
                name: "MentorClosedDate",
                table: "Mentorships");

            migrationBuilder.DropColumn(
                name: "MentorClosingComments",
                table: "Mentorships");

            migrationBuilder.DropColumn(
                name: "MentorRating",
                table: "Mentorships");
        }
    }
}
