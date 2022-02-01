using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            try
            {
                var process = new Process()
                {
                    StartInfo = new()
                    {
                        FileName = "sudo",
                        Arguments = "systemctl status wg-quick@wg0",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (string.IsNullOrEmpty(error)) { return output; }
                else { return error; }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
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
