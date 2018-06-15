using System;
using System.Collections.Generic;

namespace DSoft.AgileSprinter.Data.Models
{
    public partial class UserStories
    {
        public UserStories()
        {
            TaskHistories = new HashSet<TaskHistories>();
            Tasks = new HashSet<Tasks>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int SprintId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public decimal Estimated { get; set; }
        public string Comments { get; set; }
        public int? Priority { get; set; }
        public string Color { get; set; }

        public Projects Project { get; set; }
        public Sprints Sprint { get; set; }
        public ICollection<TaskHistories> TaskHistories { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
    }
}
