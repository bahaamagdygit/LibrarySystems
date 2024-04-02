using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dtos.Account
{
    public class RegistrationDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Name { get; set; }
        [Required]
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
        public string? RoleName { get; set; }
    }
}
