using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;

namespace MOCA_Repositories.DBContext;

public partial class MOCAContext : DbContext
{
    public MOCAContext()
    {
    }

    public MOCAContext(DbContextOptions<MOCAContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Advertisement> Advertisements { get; set; }

    public virtual DbSet<BabyTracking> BabyTrackings { get; set; }

    public virtual DbSet<BookingPayment> BookingPayments { get; set; }

    public virtual DbSet<Bookmark> Bookmarks { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<CommunityPost> CommunityPosts { get; set; }

    public virtual DbSet<CommunityReply> CommunityReplies { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseCategory> CourseCategories { get; set; }

    public virtual DbSet<CoursePayment> CoursePayments { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<DoctorBooking> DoctorBookings { get; set; }

    public virtual DbSet<DoctorContact> DoctorContacts { get; set; }

    public virtual DbSet<DoctorProfile> DoctorProfiles { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<MessagesWithDoctor> MessagesWithDoctors { get; set; }

    public virtual DbSet<MomProfile> MomProfiles { get; set; }

    public virtual DbSet<OrderCourse> OrderCourses { get; set; }

    public virtual DbSet<OrderCourseDetail> OrderCourseDetails { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<PostLike> PostLikes { get; set; }

    public virtual DbSet<PregnancyTracking> PregnancyTrackings { get; set; }

    public virtual DbSet<PurchasePackage> PurchasePackages { get; set; }

    public virtual DbSet<PurchasedCourse> PurchasedCourses { get; set; }

    public virtual DbSet<RefLect> RefLects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPregnancy> UserPregnancies { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=LUANNE\\LUANNE;Initial Catalog=MOCA;Persist Security Info=True;User ID=sa;Password=12345;TrustServerCertificate=True");

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advertisement>(entity =>
        {
            entity.HasKey(e => e.AdId).HasName("PK__Advertis__7130D5AEEAC19E1F");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.RedirectUrl).HasMaxLength(255);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<BabyTracking>(entity =>
        {
            entity.HasKey(e => e.CheckupBabyId).HasName("PK__BabyTrac__D24787C65166FBB5");

            entity.ToTable("BabyTracking");

            entity.Property(e => e.CheckupBabyId).HasColumnName("CheckupBabyID");
            entity.Property(e => e.AmnioticFluidIndex).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.EstimatedWeight).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PlacentaPosition).HasMaxLength(255);
            entity.Property(e => e.PregnancyId).HasColumnName("PregnancyID");

            entity.HasOne(d => d.Pregnancy).WithMany(p => p.BabyTrackings)
                .HasForeignKey(d => d.PregnancyId)
                .HasConstraintName("FK_BabyTracking_Pregnancy");
        });

        modelBuilder.Entity<BookingPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__BookingP__9B556A385DFE0A3C");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(100);
            entity.Property(e => e.PaymentType).HasMaxLength(100);

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingPayments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_BookingPayments_Bookings");
        });

        modelBuilder.Entity<Bookmark>(entity =>
        {
            entity.HasKey(e => e.BookmarkId).HasName("PK__Bookmark__541A3A9182DFBFC6");

            entity.Property(e => e.BookmarkId).HasColumnName("BookmarkID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Course).WithMany(p => p.Bookmarks)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Bookmarks_Courses");
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PK__Chapters__0893A36A3A5BB820");

            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Course).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Chapters_Courses");
        });

        modelBuilder.Entity<CommunityPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Communit__AA1260189D4C542C");

            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Tags).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.CommunityPosts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_CommunityPosts_Users");
        });

        modelBuilder.Entity<CommunityReply>(entity =>
        {
            entity.HasKey(e => e.ReplyId).HasName("PK__Communit__C25E46096FA36449");

            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.ParentReply).WithMany(p => p.InverseParentReply)
                .HasForeignKey(d => d.ParentReplyId)
                .HasConstraintName("FK_CommunityReplies_Parent");

            entity.HasOne(d => d.Post).WithMany(p => p.CommunityReplies)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_CommunityReplies_Posts");

            entity.HasOne(d => d.User).WithMany(p => p.CommunityReplies)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_CommunityReplies_Users");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71A73B54427E");

            entity.Property(e => e.CourseTitle).HasMaxLength(255);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Category).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Courses_Categories");

            entity.HasOne(d => d.User).WithMany(p => p.Courses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Courses_Users");
        });

        modelBuilder.Entity<CourseCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__CourseCa__19093A2B53F7E7B3");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6DF6F2E6067C");

            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.DiscountType).HasConversion<string>().HasMaxLength(50);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Value).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<DoctorBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__DoctorBo__73951AED1152B807");

            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RequiredDeposit).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorBookings)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_DoctorBookings_Doctors");

            entity.HasOne(d => d.User).WithMany(p => p.DoctorBookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_DoctorBookings_Users");
        });

        modelBuilder.Entity<DoctorContact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__DoctorCo__5C66259BD78292D6");

            entity.Property(e => e.ContactDate).HasColumnType("datetime");
            entity.Property(e => e.ContactMethod).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorContacts)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK_DoctorContacts_Doctors");

            entity.HasOne(d => d.User).WithMany(p => p.DoctorContacts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_DoctorContacts_Users");
        });

        modelBuilder.Entity<DoctorProfile>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__DoctorPr__2DC00EBFF45FC243");

            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Specialization).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.DoctorProfiles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_DoctorProfiles_Users");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF6BF3DC92B");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");

            entity.HasOne(d => d.Course)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Feedback_Course");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lessons__B084ACD0C4D2DFDE");

            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.VideoUrl).HasMaxLength(255);

            entity.HasOne(d => d.Chapter).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ChapterId)
                .HasConstraintName("FK_Lessons_Chapters");
        });

        modelBuilder.Entity<MessagesWithDoctor>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C0C9CF44BE54D");

            entity.ToTable("MessagesWithDoctor");

            entity.Property(e => e.SendAt).HasColumnType("datetime");
            entity.Property(e => e.SenderType).HasMaxLength(20);

            entity.HasOne(d => d.Contact).WithMany(p => p.MessagesWithDoctors)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK_MessagesWithDoctor_Contact");
        });

        modelBuilder.Entity<MomProfile>(entity =>
        {
            entity.HasKey(e => e.MomId).HasName("PK__MomProfi__C5D0E583F1939CBC");

            entity.Property(e => e.MomId).HasColumnName("MomID");
            entity.Property(e => e.BloodType).HasMaxLength(20);
            entity.Property(e => e.MaritalStatus).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.MomProfiles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_MomProfiles_Users");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Packages__322035ECBAA07F41");

            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.PackageName).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<PostLike>(entity =>
        {
            entity.HasKey(e => e.LikeId).HasName("PK__PostLike__A2922C145CDB73B7");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Post).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_PostLikes_Posts");

            entity.HasOne(d => d.User).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_PostLikes_Users");
        });

        modelBuilder.Entity<PregnancyTracking>(entity =>
        {
            entity.HasKey(e => e.TrackingId).HasName("PK__Pregnanc__3C19EDD191DDE92B");

            entity.ToTable("PregnancyTracking");

            entity.Property(e => e.TrackingId).HasColumnName("TrackingID");
            entity.Property(e => e.BellySize).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.BloodPressure).HasMaxLength(50);
            entity.Property(e => e.PregnancyId).HasColumnName("PregnancyID");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Pregnancy).WithMany(p => p.PregnancyTrackings)
                .HasForeignKey(d => d.PregnancyId)
                .HasConstraintName("FK_PregnancyTracking_Pregnancy");
        });

        modelBuilder.Entity<PurchasePackage>(entity =>
        {
            entity.HasKey(e => e.PurchasePackageId).HasName("PK__Purchase__C3B05BF013B87B00");

            entity.ToTable("PurchasePackage");

            entity.Property(e => e.PurchasePackageId).HasColumnName("PurchasePackageID");
            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Discount).WithMany(p => p.PurchasePackages)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK_PurchasePackage_Discounts");

            entity.HasOne(d => d.Package).WithMany(p => p.PurchasePackages)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK_PurchasePackage_Packages");

            entity.HasOne(d => d.User).WithMany(p => p.PurchasePackages)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_PurchasePackage_Users");
        });

        modelBuilder.Entity<PurchasedCourse>(entity =>
        {
            entity.HasKey(e => e.PurchasedId).HasName("PK__Purchase__2B7C245C31F15238");

            entity.Property(e => e.PurchasedId).HasColumnName("PurchasedID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.PurchasedCourses)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_PurchasedCourses_Courses");

            entity.HasOne(d => d.User).WithMany(p => p.PurchasedCourses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_PurchasedCourses_Users");
        });

        modelBuilder.Entity<RefLect>(entity =>
        {
            entity.HasKey(e => e.RefLectId).HasName("PK__RefLect__42A5357DE77C4085");

            entity.ToTable("RefLect");

            entity.Property(e => e.RefLectId).HasColumnName("RefLectID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.RefLects)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RefLect_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A2377654B");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACC73252E8");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<UserPregnancy>(entity =>
        {
            entity.HasKey(e => e.PregnancyId).HasName("PK__UserPreg__06D66108F8C73EC0");

            entity.Property(e => e.PregnancyId).HasColumnName("PregnancyID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.MomId).HasColumnName("MomID");

            entity.HasOne(d => d.Mom).WithMany(p => p.UserPregnancies)
                .HasForeignKey(d => d.MomId)
                .HasConstraintName("FK_UserPregnancies_Users");
        });

        modelBuilder.Entity<CoursePayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK_CoursePayment");

            entity.Property(e => e.PaymentCode).HasMaxLength(100);
            entity.Property(e => e.TransactionIdResponse).HasMaxLength(100);
            entity.Property(e => e.PaymentGateway).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Amount).HasColumnType("float");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.OrderCourse)
                .WithMany(p => p.CoursePayments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_CoursePayments_OrderCourses");
        });


        modelBuilder.Entity<OrderCourse>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderCourse");

            entity.Property(e => e.OrderPrice).HasColumnType("decimal(18,2)");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.User)
                .WithMany(p => p.OrderCourses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_OrderCourses_Users");

            entity.HasOne(o => o.Discount)
                .WithMany(d => d.OrderCourses)
                .HasForeignKey(o => o.DiscountId)
                .HasConstraintName("FK_OrderCourses_Discounts");

            entity.HasMany(d => d.OrderCourseDetails)
                .WithOne(p => p.OrderCourse)
                .HasForeignKey(p => p.OrderId)
                .HasConstraintName("FK_OrderCourseDetails_OrderCourses");

            entity.HasMany(d => d.CoursePayments)
                .WithOne(p => p.OrderCourse)
                .HasForeignKey(p => p.OrderId)
                .HasConstraintName("FK_CoursePayments_OrderCourses");
        });

        modelBuilder.Entity<OrderCourseDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderCourseDetail");

            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

            entity.HasOne(d => d.OrderCourse)
                .WithMany(p => p.OrderCourseDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderCourseDetails_OrderCourses");

            entity.HasOne(d => d.Course)
                .WithMany(p => p.OrderCourseDetails)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_OrderCourseDetails_Courses");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
