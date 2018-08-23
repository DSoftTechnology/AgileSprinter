using System;
using System.Collections.Generic;

namespace DSoft.AgileSprinter.Data.Models
{
    public partial class Roles
    {
        public Roles()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
