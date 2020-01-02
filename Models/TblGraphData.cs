using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class TblGraphData
    {
        public int GraphDataId { get; set; }
        public int? ParentId { get; set; }
        public int? ParentLevel { get; set; }
        public int? DtMonth { get; set; }
        public int? DtYear { get; set; }
        public string Remarks { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public decimal? DtValue { get; set; }
    }
}
