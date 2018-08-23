using System;
using System.Collections.Generic;

namespace DSoft.AgileSprinter.Data.Models
{
    public partial class TaskHistories
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserStoryId { get; set; }
        public string Description { get; set; }
        public decimal Estimated { get; set; }
        public decimal Actual { get; set; }
        public decimal Remaining { get; set; }
        public string AssignedTo { get; set; }
        public int Status { get; set; }
        public DateTime DateChanged { get; set; }
        public string Notes { get; set; }

        public Tasks Task { get; set; }
        public UserStories UserStory { get; set; }
    }
}
