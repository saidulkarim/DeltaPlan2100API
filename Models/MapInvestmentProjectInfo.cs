using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class MapInvestmentProjectInfo
    {
        public int AutoId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectObjectives { get; set; }
        public decimal? EstimatedCost { get; set; }
        public string ProjectIntervention { get; set; }
        public int? Duration { get; set; }
        public string Benefits { get; set; }
        public string District { get; set; }
        public string Upazila { get; set; }
        public string ResponsibleMinistry { get; set; }
        public string ExecutingAgency { get; set; }
        public string Hotspot { get; set; }
        public int? IsProjectActive { get; set; }
    }
}
