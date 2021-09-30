using System;
using System.Collections.Generic;
using System.Text;

namespace BoredShared.Models
{
    public class ActivityFormRequest
    {
        public string Type { get; set; }
        public int Participants { get; set; }
        public string Price { get; set; }
    }
}
