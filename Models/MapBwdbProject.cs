using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class MapBwdbProject
    {
        public int Gid { get; set; }
        public double? Area { get; set; }
        public long? Scode { get; set; }
        public string Zone { get; set; }
        public string Sname { get; set; }
        public decimal? Lemb { get; set; }
        public decimal? Lcan { get; set; }
        public decimal? Rs { get; set; }
        public decimal? Nopump { get; set; }
        public decimal? Notubew { get; set; }
        public decimal? Nevglock { get; set; }
        public string Oldtype { get; set; }
        public string Newtype { get; set; }
        public string Typecode { get; set; }
        public decimal? Parea { get; set; }
        public decimal? Bairig { get; set; }
        public decimal? Badr { get; set; }
        public decimal? Bafc { get; set; }
        public string Ystart { get; set; }
        public string Ycompl { get; set; }
        public string Value { get; set; }
        public decimal? Cwheat { get; set; }
        public string Status { get; set; }
        public string Circle { get; set; }
        public string Source { get; set; }
    }
}
