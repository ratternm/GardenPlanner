using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;

namespace Capstone.Web.Controllers
{
    public class GardenAPIController : ApiController
    {
        private IDatabaseSvc _db = null;

        public GardenAPIController(IDatabaseSvc db)
        {
            _db = db;

        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        //public string Get(string plotId)
        //{
        //    List<Plant> plots = _db.GetPlantFromPlot(plotId);
        //    string output = JsonConvert.SerializeObject(plots);

        //    return output;
        //}

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}