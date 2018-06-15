using System;
using System.Collections.Generic;

namespace DSoft.AgileSprinter.Data.Models
{
    public partial class UserRoles
    {
        public int UserRoleId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }

        public Roles Role { get; set; }
        public Users UserNameNavigation { get; set; }
    }
}
