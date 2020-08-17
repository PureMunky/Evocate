﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tete.Api.Contexts;

namespace Tete.Web.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20200817121735_build")]
    partial class build
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tete.Models.Authentication.AccessRole", b =>
                {
                    b.Property<Guid>("AccessRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("CreatedBy");

                    b.Property<string>("Name");

                    b.Property<Guid>("UserId");

                    b.HasKey("AccessRoleId");

                    b.ToTable("AccessRoles");
                });

            modelBuilder.Entity("Tete.Models.Authentication.Login", b =>
                {
                    b.Property<Guid>("LoginId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastAccessed");

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("Tete.Models.Authentication.Session", b =>
                {
                    b.Property<Guid>("SessionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Token")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("SessionId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Tete.Models.Authentication.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<byte[]>("Salt")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tete.Models.Config.Flag", b =>
                {
                    b.Property<string>("Key")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Data")
                        .HasMaxLength(200);

                    b.Property<DateTime>("Modified");

                    b.Property<bool>("Value");

                    b.HasKey("Key");

                    b.ToTable("Flags");
                });

            modelBuilder.Entity("Tete.Models.Config.Setting", b =>
                {
                    b.Property<string>("Key")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30);

                    b.Property<string>("Value")
                        .HasMaxLength(100);

                    b.HasKey("Key");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Tete.Models.Content.Topic", b =>
                {
                    b.Property<Guid>("TopicId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<bool>("Elligible");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TopicId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("Tete.Models.Localization.Element", b =>
                {
                    b.Property<Guid>("ElementId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Key");

                    b.Property<Guid>("LanguageId");

                    b.Property<string>("Text");

                    b.HasKey("ElementId");

                    b.HasIndex("LanguageId");

                    b.ToTable("Elements");
                });

            modelBuilder.Entity("Tete.Models.Localization.Language", b =>
                {
                    b.Property<Guid>("LanguageId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Name");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Tete.Models.Localization.UserLanguage", b =>
                {
                    b.Property<Guid>("UserLanguageId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("LanguageId");

                    b.Property<int>("Priority");

                    b.Property<bool>("Read");

                    b.Property<bool>("Speak");

                    b.Property<Guid>("UserId");

                    b.HasKey("UserLanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("UserLanguages");
                });

            modelBuilder.Entity("Tete.Models.Logging.Log", b =>
                {
                    b.Property<Guid>("LogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("Description");

                    b.Property<string>("Domain");

                    b.Property<string>("MachineName");

                    b.Property<DateTime>("Occured");

                    b.Property<string>("StackTrace");

                    b.HasKey("LogId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Tete.Models.Relationships.Mentorship", b =>
                {
                    b.Property<Guid>("MentorshipId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("LearnerClosed");

                    b.Property<DateTime>("LearnerClosedDate");

                    b.Property<string>("LearnerContact");

                    b.Property<Guid>("LearnerUserId");

                    b.Property<bool>("MentorClosed");

                    b.Property<DateTime>("MentorClosedDate");

                    b.Property<string>("MentorContact");

                    b.Property<Guid>("MentorUserId");

                    b.Property<DateTime>("StartDate");

                    b.Property<Guid>("TopicId");

                    b.HasKey("MentorshipId");

                    b.ToTable("Mentorships");
                });

            modelBuilder.Entity("Tete.Models.Relationships.UserTopic", b =>
                {
                    b.Property<Guid>("UserTopicID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Status");

                    b.Property<Guid>("TopicId");

                    b.Property<Guid>("UserId");

                    b.HasKey("UserTopicID");

                    b.ToTable("UserTopics");
                });

            modelBuilder.Entity("Tete.Models.Users.Evaluation", b =>
                {
                    b.Property<Guid>("EvaluationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<Guid>("MentorshipId");

                    b.Property<int>("Rating");

                    b.Property<Guid>("UserId");

                    b.Property<int>("UserType");

                    b.HasKey("EvaluationId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("Tete.Models.Users.Profile", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<string>("PrivateAbout");

                    b.Property<Guid>("UserId");

                    b.HasKey("ProfileId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Tete.Models.Localization.Element", b =>
                {
                    b.HasOne("Tete.Models.Localization.Language")
                        .WithMany("Elements")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Tete.Models.Localization.UserLanguage", b =>
                {
                    b.HasOne("Tete.Models.Localization.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}