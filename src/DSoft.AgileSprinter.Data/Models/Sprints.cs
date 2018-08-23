using System;
using System.Collections.Generic;

namespace DSoft.AgileSprinter.Data.Models
{
    public partial class Sprints
    {
        public Sprints()
        {
            UserStories = new HashSet<UserStories>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Status { get; set; }
        public bool IsInitialized { get; set; }

        public ICollection<UserStories> UserStories { get; set; }
    }
}
