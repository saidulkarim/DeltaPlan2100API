using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaPlan2100API.Models
{
    public class ClimateScenarioSubItemList
    {
        public int subitem_id { get; set; }
        public string subitem_name { get; set; }
        public string subitem_unit { get; set; }
        public string subitem_description { get; set; }
        public string error_status { get; set; }
        public string error_msg { get; set; }
    }
}
