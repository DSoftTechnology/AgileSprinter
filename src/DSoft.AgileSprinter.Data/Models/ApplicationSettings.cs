using System;
using System.Collections.Generic;

namespace DSoft.AgileSprinter.Data.Models
{
    public partial class ApplicationSettings
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
