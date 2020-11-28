using Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IConfiguration _Configuration;

        public DataController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var query = new QueryContext(_Configuration.GetConnectionString("Default"));
            IEnumerable<TransactionHistory> data = await query.GetAll();
            return Ok(data);
        }
    }
}
