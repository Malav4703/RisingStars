using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RisingStarsData.Migrations
{
    /// <inheritdoc />
    public partial class Announcement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    AnnouncementId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    PostDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.AnnouncementId);
                });

            migrationBuilder.InsertData(
                table: "Announcements",
                columns: new[] { "AnnouncementId", "Content", "PostDate", "Title" },
                values: new object[,]
                {
                    { 1, "We are excited to have you join our community of students and mentors. We hope you find the resources and support you need to succeed in your academic and professional endeavors.", new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Welcome to Rising Stars!" },
                    { 2, "We are constantly working to improve the Rising Stars platform. Keep an eye out for new features and updates!", new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "New Features" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");
        }
    }
}
