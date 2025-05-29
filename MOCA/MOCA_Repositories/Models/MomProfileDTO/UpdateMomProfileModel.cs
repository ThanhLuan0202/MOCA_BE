using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.MomProfileDTO
{
    public class UpdateMomProfileModel
    {
        public DateOnly? DateOfBirth { get; set; }

        public string Address { get; set; }

        public string MaritalStatus { get; set; }

        public string BloodType { get; set; }

        public string MedicalHistory { get; set; }



    }
}
 