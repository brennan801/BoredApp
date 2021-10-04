using System;
using System.Collections.Generic;
using System.Text;

namespace BoredShared.Models
{
    public class ActivityModel
    {
        public string Activity { get; set; }
        public string Type { get; set; }
        public int? Participants { get; set; }
        public double? Price { get; set; }
        public string Link { get; set; }
        public int Key { get; set; }
        public double? Accessibility { get; set; }
        public string Error { get; set; }
    }
}
