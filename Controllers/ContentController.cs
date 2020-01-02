using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaPlan2100API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaPlan2100API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly delta_plan_2100_appContext db = new delta_plan_2100_appContext();

        // GET: api/Content/GetTextTableHtmlContent/1/2/text
        [HttpGet("{parent_id}/{parent_level}/{content_type}", Name = "GetTextTableHtmlContent")]
        public string GetTextTableHtmlContent(int parent_id, int parent_level, string content_type)
        {
            string contentData = string.Empty;

            if (content_type.ToLower().Contains("text") || content_type.ToLower().Contains("table") || content_type.ToLower().Contains("html"))
            {
                try
                {
                    contentData = db.TblTabularData.Where(w => w.ParentId == parent_id && w.ParentLevel == parent_level && w.IsActive == true).Select(s => s.Contents).FirstOrDefault().ToString();
                }
                catch (Exception ex)
                {
                    contentData = "Web API (Application Server Error): " + ex.Message.ToString();
                }
            }
            else
            {
                contentData = "";
            }

            return contentData;
        }

        // GET: api/Content/GetMacroEconIndicator/1/2/1
        [HttpGet("{parent_id}/{parent_level}/{mei_type}", Name = "GetMacroEconIndicator")]
        public List<MacroEconIndicatorList> GetMacroEconIndicator(int parent_id, int parent_level, int mei_type)
        {
            List<MacroEconIndicatorList> lstMacroEconIndicator = new List<MacroEconIndicatorList>();

            try
            {
                lstMacroEconIndicator = db.TblIndicatorFyData
                                .Where(w =>
                                    w.ParentId == parent_id &&
                                    w.ParentLevel == parent_level &&
                                    w.IndicatorType == mei_type &&
                                    w.IsActive == true)
                                .Select(x => new MacroEconIndicatorList()
                                {
                                    //IndicatorAutoId = x.IndicatorAutoId,
                                    IndicatorName = x.IndicatorName,
                                    IndicatorType = x.IndicatorType
                                })
                                .Distinct()
                                .OrderBy(o => o.IndicatorName)
                                .ToList();
            }
            catch (Exception ex)
            {
                lstMacroEconIndicator = new List<MacroEconIndicatorList>();
            }

            return lstMacroEconIndicator;
        }

    }
}
