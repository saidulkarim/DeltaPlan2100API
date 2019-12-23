using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class TblDeltaMetaData
    {
        public int MetaDataId { get; set; }
        public int? ParentId { get; set; }
        public int? ParentLevel { get; set; }
        public string OvTitle { get; set; }
        public string OvAbstract { get; set; }
        public string GenTitle { get; set; }
        public string GenPurposeProduction { get; set; }
        public string GenCompleteness { get; set; }
        public string GenQuality { get; set; }
        public string GenHistOfTheDataset { get; set; }
        public string GenProcessDescription { get; set; }
        public string GenTypeOfDataset { get; set; }
        public string GenDatasetLanguage { get; set; }
        public string GenAddInfoSourceDataset { get; set; }
        public string AccessDataSourceName { get; set; }
        public string AccessDataSourceLocation { get; set; }
        public string AccessDistributionFileFormat { get; set; }
        public string AccessMediaOfDistribution { get; set; }
        public string AccessOrganization { get; set; }
        public string AccessOrgAddress { get; set; }
        public string AccessOrgEmailAddress { get; set; }
        public string AccessUseConstraints { get; set; }
        public string AccessAccessConstraints { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}
