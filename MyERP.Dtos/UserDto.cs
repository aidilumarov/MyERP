using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyERP.Dtos
{
    public class UserDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime DateRegistration { get; set; }

        [Required]
        public DateTime DateLastActivity { get; set; }
    }
}
