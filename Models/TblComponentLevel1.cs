using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class TblComponentLevel1
    {
        public int ComponentLevel1Id { get; set; }
        public string ComponentName { get; set; }
        public string DataVisualization { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}
