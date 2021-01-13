using Microsoft.AspNetCore.Mvc;
using MSSQLServerMonitoring.Domain.QueryModel;
using MSSQLServerMonitoring.Infrastructure.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuerySQLServerController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public QuerySQLServerController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET: api/<ValuesController 
        [HttpGet]
        public async Task<List<Query>> ListAll()
        {
            List<Query> requests = await _repositoryWrapper.Query.GetAll();

            return requests;
        }

        //GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
