using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Generic;
using MOCA_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Repositories
{
    public class DoctorContactRepository : GenericRepository<DoctorContact>, IDoctorContactRepository
    {
        private readonly MOCAContext _context;
        private readonly IDoctorProfileRepository _doctorProfileRepository;
        public DoctorContactRepository(MOCAContext context, IDoctorProfileRepository _repo)
        {
            _context = context;
            _doctorProfileRepository = _repo;
        }
        public async Task<DoctorContact> CreateDoctorContact(string userId, DoctorContact newDoctorContact)
        {
            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");
            }
            var checkDoctor = await _doctorProfileRepository.GetDoctorProfileByUserIdAsync(userId);
            
            var newDoctorContactt = new DoctorContact
            {
                UserId = newDoctorContact.UserId,
                DoctorId = checkDoctor.DoctorId,
                ContactDate = DateTime.UtcNow,
                ContactMethod = newDoctorContact.ContactMethod,
                Status = "Active"
            };

            await _context.AddAsync(newDoctorContactt);
            await _context.SaveChangesAsync();
            
            return newDoctorContact;
        }
        public async Task<DoctorContact> CompleteDoctorContact(int idContact)
        {
            var contact = await _context.DoctorContacts.FirstOrDefaultAsync(x => x.ContactId == idContact);
            if (contact == null)
            {
                throw new KeyNotFoundException("Contact not found");
            }
            
            contact.Status = "Complete";
            _context.Entry(contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return contact;
        }
    }
   
}
