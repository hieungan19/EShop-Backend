﻿namespace EShop.DTOs.Account
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public bool IsInRole { get; set; }
        public string? Address { get; set; }   
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }  

    }
}
