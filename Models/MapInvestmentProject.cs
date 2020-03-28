using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class MapInvestmentProject
    {
        public int ShapeId { get; set; }
        public long? Objectid { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public string Code { get; set; }
        public decimal? ShapeLeng { get; set; }
        public decimal? ShapeArea { get; set; }
        public string Shape { get; set; }
    }
}
