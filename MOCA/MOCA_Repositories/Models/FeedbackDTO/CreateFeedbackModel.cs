using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.FeedbackDTO
{
    public class CreateFeedbackModel
    {
        public int? CourseId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
    }
}
