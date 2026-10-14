using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllByShahar.Models
{
    public class ApplicationUser : IdentityUser
    {//מחלקה לייצוג הרחבה של משתמשי האתר

        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }//שם פרטי
                                             //--------------------------------------
                                             //שם משפחה
        [Display(Name = " שם משפחה")]
        public string LastName { get; set; }
    }
}
