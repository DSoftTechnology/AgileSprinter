using System;
using System.Collections.Generic;

namespace DSoft.AgileSprinter.Data.Models
{
    public partial class Tasks
    {
        public Tasks()
        {
            TaskHistories = new HashSet<TaskHistories>();
        }

        public int Id { get; set; }
        public int UserStoryId { get; set; }
        public string Description { get; set; }
        public decimal Estimated { get; set; }
        public decimal Actual { get; set; }
        public decimal Remaining { get; set; }
        public string AssignedTo { get; set; }
        public int Status { get; set; }
        public int Roadblocked { get; set; }
        public string Notes { get; set; }
        public int SortOrder { get; set; }

        public Users AssignedToNavigation { get; set; }
        public UserStories UserStory { get; set; }
        public ICollection<TaskHistories> TaskHistories { get; set; }
    }
}
