using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA_Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddPregnancyDiaryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PregnancyDiaries",
                columns: table => new
                {
                    PregnancyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MomID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Feeling = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PregnancyDiary", x => x.PregnancyID);
                    table.ForeignKey(
                        name: "FK_PregnancyDiary_MomProfile",
                        column: x => x.MomID,
                        principalTable: "MomProfiles",
                        principalColumn: "MomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PregnancyDiaries_MomID",
                table: "PregnancyDiaries",
                column: "MomID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PregnancyDiaries");
        }
    }
}
