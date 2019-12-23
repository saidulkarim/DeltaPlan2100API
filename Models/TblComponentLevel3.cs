﻿using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class TblComponentLevel3
    {
        public int ComponentLevel3Id { get; set; }
        public int ParentId { get; set; }
        public string ComponentName { get; set; }
        public string DataVisualization { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}
