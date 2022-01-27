using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminAPI.Controllers
{
    [Route("api/wireguard")]
    [ApiController]
    public class WireGuardController : ControllerBase
    {
        // GET: api/<WireGuardController>
        [HttpGet]
        public string Get()
        {
            return "no";
        }

        // GET api/<WireGuardController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WireGuardController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WireGuardController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WireGuardController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
