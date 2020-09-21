using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class TblAppImageData
    {
        public int ImageDataId { get; set; }
        public int? MenuId { get; set; }
        public int? MenuLevel { get; set; }
        //public string ImageTitle { get; set; }
        public byte[] ImageBlob { get; set; }
    }
}
