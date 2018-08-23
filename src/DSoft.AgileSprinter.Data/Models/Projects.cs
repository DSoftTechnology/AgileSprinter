using System;
using System.Collections.Generic;

namespace DSoft.AgileSprinter.Data.Models
{
    public partial class Projects
    {
        public Projects()
        {
            UserStories = new HashSet<UserStories>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public ICollection<UserStories> UserStories { get; set; }
    }
}
