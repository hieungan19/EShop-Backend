namespace EShop.DTOs.Account
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
       
    }
}
