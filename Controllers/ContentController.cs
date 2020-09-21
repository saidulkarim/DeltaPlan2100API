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
                string dataQuery = @"SELECT DISTINCT fiscal_year FROM public.tbl_indicator_fy_data ORDER BY fiscal_year;";

                using DbCommand command = db.Database.GetDbConnection().CreateCommand();
                command.CommandText = dataQuery;
                db.Database.OpenConnection();

                using DbDataReader result = command.ExecuteReader();
                while (result.Read())
                {
                    lstYear.Add(result[0].ToString());
                }
            }
            catch (Exception ex)
            {
                lstYear = new List<string>();
            }

            return lstYear;
        }

        // GET: api/Content/MacroEconIndicatorPivotData/ICOR
        [Obsolete]
        [HttpGet("{parent_id}/{indicator_name}", Name = "MacroEconIndicatorPivotData")]
        public List<MacroEconIndicatorsPivotList> MacroEconIndicatorPivotData(int parent_id, string indicator_name)
        {
            List<MacroEconIndicatorsPivotList> lstMEIPL = new List<MacroEconIndicatorsPivotList>();

            string dataQuery = @"SELECT 
	                                indicator_name,
	                                CASE WHEN indicator_type = 1 THEN 'BAU' ELSE 'BDP' END indicator_type,
                                    fy_value_unit, visual_unit, 
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
                dataQuery += " AND parent_id = " + parent_id + " AND indicator_name = '" + indicator_name + "'";
            }

            dataQuery += @" GROUP BY indicator_name, indicator_type, fy_value_unit, visual_unit
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
                        visual_unit = result[3].ToString(),
                        FY2016 = result[4].ToString().ToDecimal(),
                        FY2020 = result[5].ToString().ToDecimal(),
                        FY2021 = result[6].ToString().ToDecimal(),
                        FY2025 = result[7].ToString().ToDecimal(),
                        FY2026 = result[8].ToString().ToDecimal(),
                        FY2030 = result[9].ToString().ToDecimal(),
                        FY2031 = result[10].ToString().ToDecimal(),
                        FY2035 = result[11].ToString().ToDecimal(),
                        FY2036 = result[12].ToString().ToDecimal(),
                        FY2040 = result[13].ToString().ToDecimal(),
                        FY2041 = result[14].ToString().ToDecimal()
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

        // 30-Apr-2020
        // GET: api/Content/ClimateScenarioSubItemList
        [Obsolete]
        [HttpGet(Name = "ClimateScenarioSubItemList")]
        public List<ClimateScenarioSubItemList> ClimateScenarioSubItemList()
        {
            List<ClimateScenarioSubItemList> lstCSSL = new List<ClimateScenarioSubItemList>();

            string dataQuery = @"SELECT scenario_subitem_id, scenario_subitem_name, 
                                scenario_subitem_unit, scenario_subitem_description
                                FROM public.tbl_climate_scenario_subitem
                                ORDER BY scenario_subitem_id ASC;";

            try
            {
                using DbCommand command = db.Database.GetDbConnection().CreateCommand();
                command.CommandText = dataQuery;
                db.Database.OpenConnection();

                using DbDataReader result = command.ExecuteReader();
                while (result.Read())
                {
                    ClimateScenarioSubItemList _mei = new ClimateScenarioSubItemList
                    {
                        subitem_id = result[0].ToString().ToInt(),
                        subitem_name = result[1].ToString(),
                        subitem_unit = result[2].ToString(),
                        subitem_description = result[3].ToString()
                    };

                    lstCSSL.Add(_mei);
                }
            }
            catch (Exception ex)
            {
                lstCSSL = new List<ClimateScenarioSubItemList>();

                ClimateScenarioSubItemList _mei_ex = new ClimateScenarioSubItemList
                {
                    error_status = "error_occured",
                    error_msg = ex.Message
                };

                lstCSSL.Add(_mei_ex);
            }

            return lstCSSL;
        }

        // 29-Apr-2020
        // GET: api/Content/ClimateChangePivotData/1
        [Obsolete]
        [HttpGet("{subitem_id}", Name = "ClimateChangePivotData")]
        public List<ClimateChangePivotList> ClimateChangePivotData(int subitem_id)
        {
            List<ClimateChangePivotList> lstCCPL = new List<ClimateChangePivotList>();

            string dataQuery = @"SELECT * 
                                FROM crosstab('SELECT scenario_data_year, scenario_scale_id, SUM(scenario_data_value)
                                FROM public.tbl_climate_scenario_yearwise_detail
                                WHERE scenario_subitem_id = " + subitem_id + @"
                                GROUP BY scenario_scale_id, scenario_data_year
                                ORDER BY scenario_data_year ASC, scenario_scale_id ASC') 
                                AS final_result(scenario_data_year integer, ""moderate"" NUMERIC, ""productive"" NUMERIC, 
                                ""active"" NUMERIC, ""resilient"" NUMERIC);";

            try
            {
                using DbCommand command = db.Database.GetDbConnection().CreateCommand();
                command.CommandText = dataQuery;
                db.Database.OpenConnection();

                using DbDataReader result = command.ExecuteReader();
                while (result.Read())
                {
                    ClimateChangePivotList _mei = new ClimateChangePivotList
                    {
                        scenario_data_year = result[0].ToString().ToInt(),
                        moderate = result[1].ToString().ToDecimal(),
                        productive = result[2].ToString().ToDecimal(),
                        active = result[3].ToString().ToDecimal(),
                        resilient = result[4].ToString().ToDecimal()
                    };

                    lstCCPL.Add(_mei);
                }
            }
            catch (Exception ex)
            {
                lstCCPL = new List<ClimateChangePivotList>();

                ClimateChangePivotList _mei_ex = new ClimateChangePivotList
                {
                    error_status = "error_occured",
                    error_msg = ex.Message
                };

                lstCCPL.Add(_mei_ex);
            }

            return lstCCPL;
        }

        // GET: api/Content/InvestmentProjectHotspotList
        [HttpGet(Name = "InvestmentProjectHotspotList")]
        public List<InvestmentProjectList> InvestmentProjectHotspotList()
        {
            List<InvestmentProjectList> lstHotSpot = new List<InvestmentProjectList>();

            string dataQuery = @"SELECT '' AS code, 'Choose...' AS name
                                UNION ALL
                                SELECT hotspot AS code, hotspot AS name FROM (
	                                SELECT DISTINCT hotspot
	                                FROM public.map_investment_project_info
	                                WHERE hotspot IS NOT NULL AND hotspot != '' AND is_project_active = 1
	                                ORDER BY hotspot ASC
                                ) AS hotspot;";

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

                    lstHotSpot.Add(_mei);
                }
            }
            catch (Exception ex)
            {
                lstHotSpot = new List<InvestmentProjectList>();

                InvestmentProjectList _mei_ex = new InvestmentProjectList
                {
                    Code = "",
                    Name = ex.Message
                };

                lstHotSpot.Add(_mei_ex);
            }

            return lstHotSpot;
        }

        // GET: api/Content/InvestmentProjectList/hotspot
        [HttpGet("{hotspot}", Name = "InvestmentProjectList")]
        public List<InvestmentProjectList> InvestmentProjectList(string hotspot)
        {
            List<InvestmentProjectList> lstInvProj = new List<InvestmentProjectList>();

            string dataQuery = @"SELECT '' AS code, 'Choose a project...' AS name
                                 UNION ALL
                                 SELECT distinct p.code, p.title
                                 FROM public.map_investment_project p
                                 INNER JOIN public.map_investment_project_info i ON p.code = i.project_code
                                 WHERE i.hotspot = '" + hotspot + @"'
                                 ORDER BY code;";

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
	                                'type', 'Feature',                                    
	                                'geometry', ST_AsGeoJSON((shape), 15, 0)::jsonb,
	                                'properties', to_jsonb(row) - 'shape'
                                  ) AS feature 
                                FROM (
	                                SELECT p.code AS ""1. Code"", 
                                    p.title AS ""2. Title"", 
	                                i.project_objectives AS ""3. Objectives"", 
	                                i.duration AS ""4. Duration"", 
	                                i.estimated_cost AS ""5. Estimated Cost"", 
	                                i.responsible_ministry AS ""6. Responsible Ministry"", 
	                                i.executing_agency AS ""7. Executing Agency"", 
	                                p.remarks AS ""8. Remarks"", 
	                                p.shape
                                    FROM public.map_investment_project p
                                    LEFT JOIN public.map_investment_project_info i ON p.code = i.project_code
                                    WHERE p.code = '" + code + "' " + @"
                                ) row) features; ";

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

        // GET: api/Content/BwdbProjectLayer
        //[HttpGet("{code}", Name = "BwdbProjectLayer")]
        public string BwdbProjectLayer()
        {
            string response = string.Empty;
            string dataQuery = @"SELECT jsonb_build_object(
	                                'type',     'FeatureCollection',
	                                'features', jsonb_agg(feature)
                                )
                                FROM (
                                  SELECT jsonb_build_object(
	                                'type', 'Feature',                                    
	                                'geometry', ST_AsGeoJSON((shape), 15, 0)::jsonb,
	                                'properties', to_jsonb(row) - 'shape'
                                  ) AS feature 
                                FROM (" + Environment.NewLine + GenerateMapViewColumns("map_bwdb_project") + Environment.NewLine + ") row) features;";

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

        // GET: api/Content/LgedProjectLayer
        //[HttpGet("{code}", Name = "LgedProjectLayer")]
        public string LgedProjectLayer()
        {
            string response = string.Empty;
            string dataQuery = @"SELECT jsonb_build_object(
	                                'type',     'FeatureCollection',
	                                'features', jsonb_agg(feature)
                                )
                                FROM (
                                  SELECT jsonb_build_object(
	                                'type', 'Feature',                                    
	                                'geometry', ST_AsGeoJSON((shape), 15, 0)::jsonb,
	                                'properties', to_jsonb(row) - 'shape'
                                  ) AS feature 
                                FROM 
(" + Environment.NewLine + GenerateMapViewColumns("map_lged_project") + Environment.NewLine + ") row) features;";

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

        public string GenerateMapViewColumns(string table_name)
        {
            string response = string.Empty;
            string dataQuery = @"SELECT column_name, alias_name, view_serial
                                FROM public.tbl_map_view_config
                                WHERE table_name = '" + table_name + @"'
                                ORDER BY view_serial ASC;";

            try
            {
                using DbCommand command = db.Database.GetDbConnection().CreateCommand();
                command.CommandText = dataQuery;
                db.Database.OpenConnection();
                using DbDataReader result = command.ExecuteReader();

                if (result.HasRows)
                {
                    response = "SELECT " + Environment.NewLine;
                    while (result.Read())
                    {
                        response += "t." + result[0].ToString() + " AS \"" + result[2].ToString() + ". " + result[1].ToString() + "\", " + Environment.NewLine;
                    }
                    response += "t.shape " + Environment.NewLine;
                    response += "FROM public." + table_name + " t";
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }

        // GET: api/Content/GetImageData/
        [HttpGet("{menuid}/{menulevel}", Name = "GetImageData")]
        public string GetImageData(int menuid, int menulevel)
        {
            string result = string.Empty;

            try
            {
                byte[] data = db.TblAppImageData
                            .Where(w => w.MenuId == menuid && w.MenuLevel == menulevel)
                            .Select(s => s.ImageBlob)
                            .FirstOrDefault();

                result = Convert.ToBase64String(data);
            }
            catch (Exception)
            {
                result = string.Empty;
            }

            return result;
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
