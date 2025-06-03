using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.AdvertisementDTO
{
    public class CreateAdvertisementModel
    {
        public string? Title { get; set; }

        public string? ImageUrl { get; set; }

        public string? RedirectUrl { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsVisible { get; set; }
    }
}
