using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTOs
{
    public class RegisterDto
    {
        [Required] public string Username { get; set; }
        [Required] public string Firstname { get; set; }
        [Required] public string Lastname { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}