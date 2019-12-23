using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class District
    {
        public int DistrictId { get; set; }
        public int DivisionId { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
    }
}
