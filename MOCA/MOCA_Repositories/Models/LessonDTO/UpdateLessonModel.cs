using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MOCA_Repositories.Models.LessonDTO
{
    public class UpdateLessonModel
    {
        public int? ChapterId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IFormFile? VideoURL { get; set; }
        public int? Duration { get; set; }
        public int? OrderIndex { get; set; }
        
    }
}
