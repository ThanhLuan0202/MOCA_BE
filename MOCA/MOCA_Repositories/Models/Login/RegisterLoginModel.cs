using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.Login

{
    public class RegisterLoginModel
    {
       

        public int UserId { get; set; }

        public int? RoleId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Image { get; set; }

        public string Status = "Active";


    }
}
