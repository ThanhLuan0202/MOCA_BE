using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    public class DoctorContactService : IDoctorContactService
    {
        private readonly IDoctorContactRepository _doctorContactRepository;
        public DoctorContactService(IDoctorContactRepository doctorContactRepository)
        {
            _doctorContactRepository = doctorContactRepository;
        }

        public Task<DoctorContact> CompleteDoctorContact(int idContact)
        {
            return _doctorContactRepository.CompleteDoctorContact(idContact);
        }

        public Task<DoctorContact> CreateDoctorContact(string userId, DoctorContact newDoctorContact)
        {
            return _doctorContactRepository.CreateDoctorContact(userId, newDoctorContact);
        }
    }
}
