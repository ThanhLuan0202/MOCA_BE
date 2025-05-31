using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.FeedbackDTO
{
    public class ViewFeedbackModel
    {
        public int FeedbackId { get; set; }
        public int? UserId { get; set; }
        public int? CourseId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
