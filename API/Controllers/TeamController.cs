using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class TeamController : BaseController
    {
        private readonly DataContext _context;
        public TeamController(DataContext context)
        {
            _context = context;
        }

        // GET api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            return await _context.Values.Select(x => x.Name).ToArrayAsync();
        }

        // GET api/Team/4
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // POST api/Team
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }

        // PUT api/Team/4
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // DELETE api/Team/4
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}