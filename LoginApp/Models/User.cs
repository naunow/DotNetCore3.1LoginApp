using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApp.Models
{
    /// <summary>
    /// 利用者情報
    /// </summary>
    public class User
    {
        [DisplayName("User ID")]
        public int Id { get; set; }

        [DisplayName("Login ID")]
        public string Login_Id { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Age")]
        public int Age { get; set; }
    }
}
