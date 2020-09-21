using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaPlan2100API.Models.TempModels
{
    public class TblAppImageDataTemp
    {
        public int? MenuId { get; set; }
        public int? MenuLevel { get; set; }
        //public string ImageTitle { get; set; }
        public IFormFile Image { get; set; }
    }
}
