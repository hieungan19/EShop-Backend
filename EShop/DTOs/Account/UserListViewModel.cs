namespace EShop.DTOs.Account
{
    public class UserListViewModel
    {
      
            public UserListViewModel()
            {
                this.Users = new List<UserViewModel>();
            }

            public List<UserViewModel> Users { get; set; }
        }

    }


