using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.ChapterDTO
{
    public class AddChapterModel
    {
        public int? CourseId { get; set; }

        public string? Title { get; set; }

        public int? OrderIndex { get; set; }
    }
}
