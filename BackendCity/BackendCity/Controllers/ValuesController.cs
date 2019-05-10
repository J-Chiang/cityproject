using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using DataTables.Queryable;

namespace BackendCity.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        protected readonly BackendDbContext DbContext;
        
        public ValuesController(BackendDbContext db)
        {
            DbContext = db;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var y = DbContext.Users.First(x => x.Id == 20479);

            return new string[] { "value1", y.IP };
        }

        // GET api/values
        [HttpGet("Data")]
        public IActionResult Data(IDataTablesRequest request)
        {
            if (request == null)
            {
                var rr = DataTablesResponse.Create(request, "BadRequest");
                return new DataTablesJsonResult(rr, true);
                // return BadRequest();
            }
            var r = DbContext.Users.DataTableFilter(request);

            /* FIxME: actually generate the SQL dTrequest from the dTrequest 
            var data = DbContext.Users.Where(x => 20400 <= x.Id && x.Id <= 20500);

            var filteredData = data.Where(x => 20479 <= x.Id && x.Id <= 20490);

            var dataPage = filteredData.Skip(dTrequest.Start).Take(dTrequest.Length);

            var response = DataTablesResponse.Create(dTrequest, data.Count(), filteredData.Count(), dataPage);

            // Easier way is to return a new 'DataTablesJsonResult', which will automatically convert your
            // response to a json-compatible content, so DataTables can read it when received.
            return new DataTablesJsonResult(response, true);
            */

            // r.ToPagedListAsync();

            var a = r.ToPagedList();

            return new DataTablesJsonResult(a.ToResponse(DbContext.Users.Count()), true);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
