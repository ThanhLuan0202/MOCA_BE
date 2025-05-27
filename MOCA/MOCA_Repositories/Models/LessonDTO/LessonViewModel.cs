using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.LessonDTO
{
    public class LessonViewModel
    {
        public int LessonId { get; set; }
        public int? ChapterId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? VideoURL { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? Duration { get; set; }
        public int? OrderIndex { get; set; }
        public string Status { get; set; }
    }
}
