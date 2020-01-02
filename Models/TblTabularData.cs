using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class TblTabularData
    {
        public int TabularDataId { get; set; }
        public int? ParentId { get; set; }
        public int? ParentLevel { get; set; }
        public string Contents { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}
