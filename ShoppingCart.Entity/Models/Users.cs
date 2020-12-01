using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Entity
{
    [MetadataType(typeof(UserMetaData))]
    public partial class Users
    {
       
    }

    public class UserMetaData
    {
        public int UserId { get; set; }
        
        public string UserName { get; set; }
        [DisplayName("Email")]
        public string UserEmail { get; set; }
        [DisplayName("Password")]
        public string UserPassword { get; set; }
        public Nullable<bool> IsActive { get; set; }
        [DisplayName("Contact Number")]
        public string UserContact { get; set; }
    }
}
