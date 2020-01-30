using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DeltaPlan2100API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaPlan2100API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        // GET: api/Map/GetLgedMapLayer/
        [HttpGet(Name = "GetLgedMapLayer")]
        public string GetLgedMapLayer()
        {
            string collection = string.Empty;

            try
            {
                string reqUrl = "http://202.53.173.179:9090/geoserver/BDP/ows?service=WFS&version=1.0.0&request=GetFeature&typeName=BDP:LGEDProjectBoundary&maxFeatures=50&outputFormat=application/json";

                MapApiWebRequest myRequest = new MapApiWebRequest(reqUrl);
                collection = myRequest.GetResponse();
            }
            catch (Exception ex)
            {
                collection = ex.Message.ToString();
            }

            return collection;
        }

        // GET: api/Map
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Map/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Map
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Map/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
