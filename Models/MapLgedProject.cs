using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class MapLgedProject
    {
        public int Gid { get; set; }
        public double? Area { get; set; }
        public string Spid { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Upazila { get; set; }
        public string Spname { get; set; }
        public string Sptype { get; set; }
        public int? Garea { get; set; }
        public long? Barea { get; set; }
        public string Wmcaname { get; set; }
        public long? Wmcaregion { get; set; }
        public DateTime? Wmacrcode { get; set; }
        public int? Benefhhm { get; set; }
        public double? Mmale { get; set; }
        public int? Mfemale { get; set; }
        public int? Capfdsha { get; set; }
        public int? Capfdsav { get; set; }
        public double? Capfdtotal { get; set; }
        public string Mloan { get; set; }
        public string Floan { get; set; }
        public string Lamount { get; set; }
        public string Lrealize { get; set; }
        public string Zrealized { get; set; }
        public string Omfund { get; set; }
        public string Phase { get; set; }
    }
}
