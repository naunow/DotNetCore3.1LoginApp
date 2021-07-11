using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApp.ViewModels
{
    public class ViewUser
    {
        [DisplayName("User ID")]
        public int Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Age")]
        public int Age { get; set; }
    }
}
