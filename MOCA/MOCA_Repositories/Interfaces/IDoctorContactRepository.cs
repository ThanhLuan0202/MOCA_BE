using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IDoctorContactRepository
    {
        Task<DoctorContact> CreateDoctorContact(string userId, DoctorContact newDoctorContact);
        Task<DoctorContact> CompleteDoctorContact(int idContact);


    }
}
