using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA_Repositories.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    AdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RedirectUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Advertis__7130D5AEEAC19E1F", x => x.AdId);
                });

            migrationBuilder.CreateTable(
                name: "CourseCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CourseCa__19093A2B53F7E7B3", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    MaxUsage = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Discount__E43F6DF6F2E6067C", x => x.DiscountID);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Packages__322035ECBAA07F41", x => x.PackageID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE3A2377654B", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCACC73252E8", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "CommunityPosts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Communit__AA1260189D4C542C", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_CommunityPosts_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CourseTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Courses__C92D71A73B54427E", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Categories",
                        column: x => x.CategoryId,
                        principalTable: "CourseCategories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK_Courses_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "DoctorProfiles",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DoctorPr__2DC00EBFF45FC243", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_DoctorProfiles_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "MomProfiles",
                columns: table => new
                {
                    MomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BloodType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MomProfi__C5D0E583F1939CBC", x => x.MomID);
                    table.ForeignKey(
                        name: "FK_MomProfiles_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "PurchasePackage",
                columns: table => new
                {
                    PurchasePackageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DiscountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Purchase__C3B05BF013B87B00", x => x.PurchasePackageID);
                    table.ForeignKey(
                        name: "FK_PurchasePackage_Discounts",
                        column: x => x.DiscountID,
                        principalTable: "Discounts",
                        principalColumn: "DiscountID");
                    table.ForeignKey(
                        name: "FK_PurchasePackage_Packages",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID");
                    table.ForeignKey(
                        name: "FK_PurchasePackage_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "RefLect",
                columns: table => new
                {
                    RefLectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    TextReport = table.Column<int>(type: "int", nullable: true),
                    ImageReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RefLect__42A5357DE77C4085", x => x.RefLectID);
                    table.ForeignKey(
                        name: "FK_RefLect_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "CommunityReplies",
                columns: table => new
                {
                    ReplyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    ParentReplyId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Communit__C25E46096FA36449", x => x.ReplyId);
                    table.ForeignKey(
                        name: "FK_CommunityReplies_Parent",
                        column: x => x.ParentReplyId,
                        principalTable: "CommunityReplies",
                        principalColumn: "ReplyId");
                    table.ForeignKey(
                        name: "FK_CommunityReplies_Posts",
                        column: x => x.PostId,
                        principalTable: "CommunityPosts",
                        principalColumn: "PostId");
                    table.ForeignKey(
                        name: "FK_CommunityReplies_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "PostLikes",
                columns: table => new
                {
                    LikeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PostLike__A2922C145CDB73B7", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_PostLikes_Posts",
                        column: x => x.PostId,
                        principalTable: "CommunityPosts",
                        principalColumn: "PostId");
                    table.ForeignKey(
                        name: "FK_PostLikes_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Bookmarks",
                columns: table => new
                {
                    BookmarkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bookmark__541A3A9182DFBFC6", x => x.BookmarkID);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Courses",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    ChapterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Chapters__0893A36A3A5BB820", x => x.ChapterId);
                    table.ForeignKey(
                        name: "FK_Chapters_Courses",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__6A4BEDF6BF3DC92B", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK_Feedback_Course",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                });

            migrationBuilder.CreateTable(
                name: "PurchasedCourses",
                columns: table => new
                {
                    PurchasedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DiscountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Purchase__2B7C245C31F15238", x => x.PurchasedID);
                    table.ForeignKey(
                        name: "FK_PurchasedCourses_Courses",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_PurchasedCourses_Discounts",
                        column: x => x.DiscountID,
                        principalTable: "Discounts",
                        principalColumn: "DiscountID");
                    table.ForeignKey(
                        name: "FK_PurchasedCourses_Users",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "DoctorBookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ConsultationType = table.Column<byte>(type: "tinyint", nullable: true),
                    RequiredDeposit = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DoctorBo__73951AED1152B807", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_DoctorBookings_Doctors",
                        column: x => x.DoctorId,
                        principalTable: "DoctorProfiles",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_DoctorBookings_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "DoctorContacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    ContactDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ContactMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DoctorCo__5C66259BD78292D6", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_DoctorContacts_Doctors",
                        column: x => x.DoctorId,
                        principalTable: "DoctorProfiles",
                        principalColumn: "DoctorId");
                    table.ForeignKey(
                        name: "FK_DoctorContacts_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "UserPregnancies",
                columns: table => new
                {
                    PregnancyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MomID = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserPreg__06D66108F8C73EC0", x => x.PregnancyID);
                    table.ForeignKey(
                        name: "FK_UserPregnancies_Users",
                        column: x => x.MomID,
                        principalTable: "MomProfiles",
                        principalColumn: "MomID");
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChapterId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lessons__B084ACD0C4D2DFDE", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_Lessons_Chapters",
                        column: x => x.ChapterId,
                        principalTable: "Chapters",
                        principalColumn: "ChapterId");
                });

            migrationBuilder.CreateTable(
                name: "BookingPayments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookingP__9B556A385DFE0A3C", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_BookingPayments_Bookings",
                        column: x => x.BookingId,
                        principalTable: "DoctorBookings",
                        principalColumn: "BookingId");
                });

            migrationBuilder.CreateTable(
                name: "MessagesWithDoctor",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<int>(type: "int", nullable: true),
                    SenderType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Messages__C87C0C9CF44BE54D", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_MessagesWithDoctor_Contact",
                        column: x => x.ContactId,
                        principalTable: "DoctorContacts",
                        principalColumn: "ContactId");
                });

            migrationBuilder.CreateTable(
                name: "BabyTracking",
                columns: table => new
                {
                    CheckupBabyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PregnancyID = table.Column<int>(type: "int", nullable: true),
                    CheckupDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FetalHeartRate = table.Column<int>(type: "int", nullable: true),
                    EstimatedWeight = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    AmnioticFluidIndex = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    PlacentaPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DoctorComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UltrasoundImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BabyTrac__D24787C65166FBB5", x => x.CheckupBabyID);
                    table.ForeignKey(
                        name: "FK_BabyTracking_Pregnancy",
                        column: x => x.PregnancyID,
                        principalTable: "UserPregnancies",
                        principalColumn: "PregnancyID");
                });

            migrationBuilder.CreateTable(
                name: "PregnancyTracking",
                columns: table => new
                {
                    TrackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PregnancyID = table.Column<int>(type: "int", nullable: true),
                    TrackingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    WeekNumber = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    BellySize = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    BloodPressure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pregnanc__3C19EDD191DDE92B", x => x.TrackingID);
                    table.ForeignKey(
                        name: "FK_PregnancyTracking_Pregnancy",
                        column: x => x.PregnancyID,
                        principalTable: "UserPregnancies",
                        principalColumn: "PregnancyID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BabyTracking_PregnancyID",
                table: "BabyTracking",
                column: "PregnancyID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPayments_BookingId",
                table: "BookingPayments",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_CourseId",
                table: "Bookmarks",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_CourseId",
                table: "Chapters",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPosts_UserId",
                table: "CommunityPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityReplies_ParentReplyId",
                table: "CommunityReplies",
                column: "ParentReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityReplies_PostId",
                table: "CommunityReplies",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityReplies_UserId",
                table: "CommunityReplies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserID",
                table: "Courses",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorBookings_DoctorId",
                table: "DoctorBookings",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorBookings_UserId",
                table: "DoctorBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorContacts_DoctorId",
                table: "DoctorContacts",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorContacts_UserId",
                table: "DoctorContacts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_UserId",
                table: "DoctorProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CourseID",
                table: "Feedbacks",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ChapterId",
                table: "Lessons",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_MessagesWithDoctor_ContactId",
                table: "MessagesWithDoctor",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_MomProfiles_UserID",
                table: "MomProfiles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_PostId",
                table: "PostLikes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_UserId",
                table: "PostLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PregnancyTracking_PregnancyID",
                table: "PregnancyTracking",
                column: "PregnancyID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedCourses_CourseID",
                table: "PurchasedCourses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedCourses_DiscountID",
                table: "PurchasedCourses",
                column: "DiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedCourses_UserID",
                table: "PurchasedCourses",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePackage_DiscountID",
                table: "PurchasePackage",
                column: "DiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePackage_PackageID",
                table: "PurchasePackage",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePackage_UserID",
                table: "PurchasePackage",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RefLect_UserID",
                table: "RefLect",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPregnancies_MomID",
                table: "UserPregnancies",
                column: "MomID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "BabyTracking");

            migrationBuilder.DropTable(
                name: "BookingPayments");

            migrationBuilder.DropTable(
                name: "Bookmarks");

            migrationBuilder.DropTable(
                name: "CommunityReplies");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "MessagesWithDoctor");

            migrationBuilder.DropTable(
                name: "PostLikes");

            migrationBuilder.DropTable(
                name: "PregnancyTracking");

            migrationBuilder.DropTable(
                name: "PurchasedCourses");

            migrationBuilder.DropTable(
                name: "PurchasePackage");

            migrationBuilder.DropTable(
                name: "RefLect");

            migrationBuilder.DropTable(
                name: "DoctorBookings");

            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "DoctorContacts");

            migrationBuilder.DropTable(
                name: "CommunityPosts");

            migrationBuilder.DropTable(
                name: "UserPregnancies");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "DoctorProfiles");

            migrationBuilder.DropTable(
                name: "MomProfiles");

            migrationBuilder.DropTable(
                name: "CourseCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
