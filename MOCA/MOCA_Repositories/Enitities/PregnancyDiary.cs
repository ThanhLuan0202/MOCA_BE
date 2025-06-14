using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Enitities
{
    public partial class PregnancyDiary
    {
        public int PregnancyID { get; set; }

        public int MomID { get; set; }

        public string Title { get; set; }

        public string Feeling { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual MomProfile? MomProfile { get; set; }
    }
}
