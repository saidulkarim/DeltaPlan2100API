using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class TblMapViewConfig
    {
        public int AutoId { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string AliasName { get; set; }
        public int? ViewSerial { get; set; }
    }
}
