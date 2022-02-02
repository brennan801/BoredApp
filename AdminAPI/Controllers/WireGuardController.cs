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
                        FileName = "systemctl",
                        Arguments = "status wg-quick@wg0",
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
    }
    [Route("api/wireguard/restart")]
    [ApiController]
    public class WireguardRestartController : ControllerBase
    {
        // GET: api/wireguard/restart
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
                        Arguments = "systemctl restart wg-quick@wg0",
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
    }
}
