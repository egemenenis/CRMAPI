﻿using System.ComponentModel.DataAnnotations;

namespace CRMAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User";
    }
}
