using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class Course
{
    public int CourseId { get; set; }

    public int? UserId { get; set; }

    public int? CategoryId { get; set; }

    public string? CourseTitle { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Status { get; set; }

    public string? Image { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();

    public virtual CourseCategory? Category { get; set; }

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

    public virtual ICollection<PurchasedCourse> PurchasedCourses { get; set; } = new List<PurchasedCourse>();

    public virtual User? User { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderCourseDetail> OrderCourseDetails { get; set; } = new HashSet<OrderCourseDetail>();
}
