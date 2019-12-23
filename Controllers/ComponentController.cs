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
    public class ComponentController : ControllerBase
    {
        private readonly delta_plan_2100_appContext db = new delta_plan_2100_appContext();

        #region Component Level One
        // GET: api/Component/GetComLevelOne
        [HttpGet]
        public IEnumerable<TblComponentLevel1> GetComLevelOne()
        {
            var comLevelOneList = db.TblComponentLevel1.Where(w => w.IsActive == true).ToList();
            return comLevelOneList;
        }

        // GET: api/Component/GetComLevelOne/5
        [HttpGet("{id}", Name = "GetComLevelOne")]
        public TblComponentLevel1 GetComLevelOne(int id)
        {
            var comLevelOneItem = db.TblComponentLevel1.Where(w => w.ComponentLevel1Id == id && w.IsActive == true).FirstOrDefault();

            if (comLevelOneItem != null)
                return comLevelOneItem;
            else
                return null;
        }
        #endregion

        #region Component Level Two
        // GET: api/Component/GetComLevelTwo
        [HttpGet]
        public IEnumerable<TblComponentLevel2> GetComLevelTwo()
        {
            var comLevelTwoList = db.TblComponentLevel2.Where(w => w.IsActive == true).ToList();

            if (comLevelTwoList != null)
                return comLevelTwoList;
            else
                return null;
        }

        // GET: api/Component/GetComLevelTwo/5
        [HttpGet("{id}", Name = "GetComLevelTwo")]
        public IEnumerable<TblComponentLevel2> GetComLevelTwo(int id)
        {
            var comLevelTwoItem = db.TblComponentLevel2.Where(w => w.ParentId == id && w.IsActive == true).ToList();

            if (comLevelTwoItem != null)
                return comLevelTwoItem;
            else
                return null;
        }
        #endregion

        #region Component Level Three
        // GET: api/Component/GetComLevelThree
        [HttpGet]
        public IEnumerable<TblComponentLevel3> GetComLevelThree()
        {
            var comLevelThreeList = db.TblComponentLevel3.Where(w => w.IsActive == true).ToList();

            if (comLevelThreeList != null)
                return comLevelThreeList;
            else
                return null;
        }

        // GET: api/Component/GetComLevelThree/5
        [HttpGet("{id}", Name = "GetComLevelThree")]
        public IEnumerable<TblComponentLevel3> GetComLevelThree(int id)
        {
            var comLevelThreeItem = db.TblComponentLevel3.Where(w => w.ParentId == id && w.IsActive == true).ToList();

            if (comLevelThreeItem != null)
                return comLevelThreeItem;
            else
                return null;
        }
        #endregion

        //// POST: api/Component
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}
        //// PUT: api/Component/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}
        //// DELETE: api/Component/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
