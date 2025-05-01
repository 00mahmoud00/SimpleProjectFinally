using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleLibrary.Models.AdminViewModels
{
    public class AdminUserEditViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
