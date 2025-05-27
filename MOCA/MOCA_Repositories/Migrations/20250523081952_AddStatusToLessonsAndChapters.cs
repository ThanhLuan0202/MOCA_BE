using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA_Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToLessonsAndChapters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoB",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Lessons",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Chapters",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Chapters");

            migrationBuilder.AddColumn<DateTime>(
                name: "DoB",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }
    }
}
