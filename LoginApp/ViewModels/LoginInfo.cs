using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApp.ViewModels
{
    public class LoginInfo
    {
        [Required()]
        [DisplayName("Login ID")]
        public string Login_Id { get; set; }

        [Required()]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
