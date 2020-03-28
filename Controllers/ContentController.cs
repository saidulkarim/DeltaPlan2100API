using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaPlan2100API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeltaPlan2100API.Helper;
using System.Data.Common;
using DeltaPlan2100API.Models.TempModels;
using System.Drawing;
using System.Globalization;

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
                                    IndicatorType = x.IndicatorType,
                                    VisualOrder = x.VisualOrder
                                })
                                .Distinct()
                                .OrderBy(o => o.VisualOrder)
                                .ToList();
            }
            catch (Exception ex)
            {
                lstMacroEconIndicator = new List<MacroEconIndicatorList>();
            }

            return lstMacroEconIndicator;
        }

        // GET: api/Content/GetFiscalYearList/
        [HttpGet(Name = "GetFiscalYearList")]
        public List<string> GetFiscalYearList()
        {
            List<string> lstYear = new List<string>();

            try
            {
                lstYear = db.TblIndicatorFyData
                            .Select(s => s.FiscalYear.ToString())
                            .Distinct()
                            .OrderBy(o => o)
                            .ToList();
            }
            catch (Exception ex)
            {
                lstYear = new List<string>();
            }

            return lstYear;
        }

        // GET: api/Content/MacroEconIndicatorPivotData/ICOR
        [Obsolete]
        [HttpGet("{indicator_name}", Name = "MacroEconIndicatorPivotData")]
        public List<MacroEconIndicatorsPivotList> MacroEconIndicatorPivotData(string indicator_name)
        {
            List<MacroEconIndicatorsPivotList> lstMEIPL = new List<MacroEconIndicatorsPivotList>();

            string dataQuery = @"SELECT 
	                                indicator_name,
	                                CASE WHEN indicator_type = 1 THEN 'BAU' ELSE 'BDP' END indicator_type,
                                    fy_value_unit,
	                                MAX(fy_value) FILTER (WHERE fiscal_year = 2016) AS FY2016,
                                    MAX(fy_value) FILTER (WHERE fiscal_year = 2020) AS FY2020,
	                                MAX(fy_value) FILTER (WHERE fiscal_year = 2021) AS FY2021,
	                                MAX(fy_value) FILTER (WHERE fiscal_year = 2025) AS FY2025,
	                                MAX(fy_value) FILTER (WHERE fiscal_year = 2026) AS FY2026,
	                                MAX(fy_value) FILTER (WHERE fiscal_year = 2030) AS FY2030,
	                                MAX(fy_value) FILTER (WHERE fiscal_year = 2031) AS FY2031,
	                                MAX(fy_value) FILTER (WHERE fiscal_year = 2035) AS FY2035,
	                                MAX(fy_value) FILTER (WHERE fiscal_year = 2036) AS FY2036,
	                                MAX(fy_value) FILTER (WHERE fiscal_year = 2040) AS FY2040,
                                    MAX(fy_value) FILTER (WHERE fiscal_year = 2041) AS FY2041
                                FROM public.tbl_indicator_fy_data 
                                WHERE 1 = 1";

            if (!string.IsNullOrEmpty(indicator_name))
            {
                dataQuery += " AND indicator_name = '" + indicator_name + "'";
            }

            dataQuery += @" GROUP BY indicator_name, indicator_type, fy_value_unit
                            ORDER BY indicator_name, indicator_type;";

            try
            {
                using DbCommand command = db.Database.GetDbConnection().CreateCommand();
                command.CommandText = dataQuery;
                db.Database.OpenConnection();

                using DbDataReader result = command.ExecuteReader();
                while (result.Read())
                {
                    MacroEconIndicatorsPivotList _mei = new MacroEconIndicatorsPivotList
                    {
                        indicator_name = result[0].ToString(),
                        indicator_type = result[1].ToString(),
                        fy_value_unit = result[2].ToString(),
                        FY2016 = result[3].ToString().ToDecimal(),
                        FY2020 = result[4].ToString().ToDecimal(),
                        FY2021 = result[5].ToString().ToDecimal(),
                        FY2025 = result[6].ToString().ToDecimal(),
                        FY2026 = result[7].ToString().ToDecimal(),
                        FY2030 = result[8].ToString().ToDecimal(),
                        FY2031 = result[9].ToString().ToDecimal(),
                        FY2035 = result[10].ToString().ToDecimal(),
                        FY2036 = result[11].ToString().ToDecimal(),
                        FY2040 = result[12].ToString().ToDecimal(),
                        FY2041 = result[13].ToString().ToDecimal()
                    };

                    lstMEIPL.Add(_mei);
                }
            }
            catch (Exception ex)
            {
                lstMEIPL = new List<MacroEconIndicatorsPivotList>();

                MacroEconIndicatorsPivotList _mei_ex = new MacroEconIndicatorsPivotList
                {
                    indicator_name = "error_occured",
                    error = ex.Message
                };

                lstMEIPL.Add(_mei_ex);
            }

            return lstMEIPL;
        }

        // GET: api/Content/InvestmentProjectList        
        public List<InvestmentProjectList> InvestmentProjectList()
        {
            List<InvestmentProjectList> lstInvProj = new List<InvestmentProjectList>();

            string dataQuery = @"SELECT distinct code, name
	                             FROM public.map_investment_project
	                             ORDER BY name;";

            try
            {
                using DbCommand command = db.Database.GetDbConnection().CreateCommand();
                command.CommandText = dataQuery;
                db.Database.OpenConnection();

                using DbDataReader result = command.ExecuteReader();
                while (result.Read())
                {
                    InvestmentProjectList _mei = new InvestmentProjectList
                    {
                        Code = result[0].ToString(),
                        Name = result[1].ToString()
                    };

                    lstInvProj.Add(_mei);
                }
            }
            catch (Exception ex)
            {
                lstInvProj = new List<InvestmentProjectList>();

                InvestmentProjectList _mei_ex = new InvestmentProjectList
                {
                    Code = "",
                    Name = ex.Message
                };

                lstInvProj.Add(_mei_ex);
            }

            return lstInvProj;
        }

        // GET: api/Content/InvestmentProjectLayer/code
        [HttpGet("{code}", Name = "InvestmentProjectLayer")]
        public string InvestmentProjectLayer(string code)
        {
            string response = string.Empty;
            string dataQuery = @"SELECT jsonb_build_object(
                                    'type',     'FeatureCollection',
                                    'features', jsonb_agg(feature)
                                )
                                FROM (
                                  SELECT jsonb_build_object(
                                    'type',       'Feature',                                    
                                    'geometry',   ST_AsGeoJSON((shape), 15, 0)::jsonb,
                                    'properties', to_jsonb(row) - 'shape_id' - 'shape'
                                  ) AS feature 
                                FROM (SELECT * FROM public.map_investment_project where code = '" + code + "') row) features;";

            try
            {
                using DbCommand command = db.Database.GetDbConnection().CreateCommand();
                command.CommandText = dataQuery;
                db.Database.OpenConnection();
                using DbDataReader result = command.ExecuteReader();

                while (result.Read())
                {
                    response = result[0].ToString();
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }

        #region Send Feedback
        // POST: api/Content/SendFeedback
        [HttpPost]
        public string SendFeedback(string user_name, string phone_no, string user_email, string user_comment)
        {
            string result = string.Empty;
            TblUserComments tblUserComments = new TblUserComments();

            try
            {
                if (!string.IsNullOrEmpty(user_name))
                {
                    tblUserComments.UserName = user_name;
                    tblUserComments.UserPhone = phone_no;
                    tblUserComments.UserEmailAddress = user_email;
                    tblUserComments.UserComments = user_comment;

                    if (tblUserComments != null)
                    {
                        db.TblUserComments.Add(tblUserComments);
                        int x = db.SaveChanges();

                        result = x > 0 ? "success" : "failed";
                    }
                    else
                    {
                        result = "failed";
                    }
                }
                else
                {
                    result = "failed";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
        #endregion
    }
}
