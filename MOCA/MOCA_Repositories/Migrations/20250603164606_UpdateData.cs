using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA_Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedCourses_Discounts",
                table: "PurchasedCourses");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedCourses_DiscountID",
                table: "PurchasedCourses");

            migrationBuilder.DropColumn(
                name: "DiscountID",
                table: "PurchasedCourses");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "PurchasedCourses");

            migrationBuilder.AddColumn<string>(
                name: "DiscountType",
                table: "Discounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderCourses",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DiscountId = table.Column<int>(type: "int", nullable: true),
                    OrderPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderCourse", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderCourses_Discounts",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "DiscountID");
                    table.ForeignKey(
                        name: "FK_OrderCourses_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "CoursePayments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    PaymentCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TransactionIdResponse = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PaymentGateway = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePayment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_CoursePayments_OrderCourses",
                        column: x => x.OrderId,
                        principalTable: "OrderCourses",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateTable(
                name: "OrderCourseDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderCourseDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderCourseDetails_Courses",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_OrderCourseDetails_OrderCourses",
                        column: x => x.OrderId,
                        principalTable: "OrderCourses",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursePayments_OrderId",
                table: "CoursePayments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCourseDetails_CourseId",
                table: "OrderCourseDetails",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCourseDetails_OrderId",
                table: "OrderCourseDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCourses_DiscountId",
                table: "OrderCourses",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderCourses_UserId",
                table: "OrderCourses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursePayments");

            migrationBuilder.DropTable(
                name: "OrderCourseDetails");

            migrationBuilder.DropTable(
                name: "OrderCourses");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                table: "Discounts");

            migrationBuilder.AddColumn<int>(
                name: "DiscountID",
                table: "PurchasedCourses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "PurchasedCourses",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedCourses_DiscountID",
                table: "PurchasedCourses",
                column: "DiscountID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedCourses_Discounts",
                table: "PurchasedCourses",
                column: "DiscountID",
                principalTable: "Discounts",
                principalColumn: "DiscountID");
        }
    }
}
