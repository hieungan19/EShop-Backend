namespace EShop.DTOs.Account
{
    public class LoginResponseViewModel
    {
        public LoginResponseViewModel(string token, string message, UserViewModel userView)
        {
            this.Token = token;
            this.Message = message;
            this.UserInfo = userView;
        }

        public string Token { get; set; }
        public string Message { get; set; }
        public UserViewModel UserInfo { get; set; }
    }
}
