using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class TblIndicatorFyData
    {
        public int IndicatorAutoId { get; set; }
        public int? ParentId { get; set; }
        public int? ParentLevel { get; set; }
        public string IndicatorName { get; set; }
        public int? FiscalYear { get; set; }
        public decimal? FyValue { get; set; }
        public string FyValueUnit { get; set; }
        public int? IndicatorType { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public int? VisualOrder { get; set; }
    }
}
