using System;
using System.Collections.Generic;

namespace DSoft.AgileSprinter.Data.Models
{
    public partial class Users
    {
        public Users()
        {
            Tasks = new HashSet<Tasks>();
            UserRoles = new HashSet<UserRoles>();
        }

        public string Name { get; set; }
        public string NameWithDomain { get; set; }
        public string FriendlyName { get; set; }
        public string Initials { get; set; }
        public int Status { get; set; }
        public DateTime? AdsyncLastUpdate { get; set; }
        public int AdsyncStatus { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
