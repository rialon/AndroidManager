using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AndroidManager.WEB.Models {

    public class RegisterViewModel {
        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter password confirmation")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and password confirmation are not the same")]
        public string ConfirmPassword { get; set; }
    }
}