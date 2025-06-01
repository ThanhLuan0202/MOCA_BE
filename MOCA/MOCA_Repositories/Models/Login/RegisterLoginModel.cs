using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public IFormFile? Image { get; set; }

        public string Status = "Pending";


    }
}
