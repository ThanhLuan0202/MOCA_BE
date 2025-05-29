using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
namespace MOCA_Repositories.Models.ChapterDTO
{
    public class ChapterViewModel
    {
        public int ChapterId { get; set; }
        public int? CourseId { get; set; }
        public string? Title { get; set; }
        public int? OrderIndex { get; set; }
        public string? Status { get; set; }

    }
}
