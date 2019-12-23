using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class Upazilla
    {
        public int UpazillaId { get; set; }
        public int DistrictId { get; set; }
        public string UpazillaCode { get; set; }
        public string UpazillaName { get; set; }
    }
}
