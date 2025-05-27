using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.CourseDTO
{
    public class UpdateCourseModel
    {
        public int? CategoryId { get; set; }

        public string CourseTitle { get; set; }

        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }

        public string Status { get; set; }

        public string Image { get; set; }

        public decimal? Price { get; set; }
    }
}
