using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web.Models
{
    public class Membership
    {
        public int Id { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        
        public string Password { get; set; }
    }
}