using System.ComponentModel.DataAnnotations;

namespace ITI_MVC.ViewModels
{
    public class AccountViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
