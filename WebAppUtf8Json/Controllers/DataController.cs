using Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppUtf8Json.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IConfiguration _Configuration;

        public DataController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        [HttpGet]
        [Route("utf8")]
        public async Task<ActionResult> Get()
        {
            var query = new QueryContext(_Configuration.GetConnectionString("Default"));
            IEnumerable<TransactionHistory> data = await query.GetAll();
            return Ok(data);
        }
    }
}
