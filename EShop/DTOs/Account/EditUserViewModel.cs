﻿namespace EShop.DTOs.Account
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
       
    }
}
