using BoredWebAppAdmin.Models;
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
                return showWireguard();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        [HttpPost]
        public ClientInformation Post((int id, string name) clientInfo)
        {
            try
            {
                string privateKey = generatePrivateKey();
                string publicKey = generatePublicKey(privateKey);
                string addPeerResult = addNewPeer(publicKey, clientInfo.id);
                ClientInformation newClient = new()
                {
                    ID = clientInfo.id,
                    ClientName = clientInfo.name,
                    IpAddress = $"10.200.20.{clientInfo.id}",
                    DateAdded = System.DateTime.Now.ToString(),
                    AllowedIpRange = "10.200.20.*",
                    ClientPublicKey = publicKey,
                    ClientPrivateKey = privateKey
                };
                return newClient;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private string addNewPeer(string publicKey, int id)
        {
            var process = new Process()
            {
                StartInfo = new()
                {
                    FileName = "wg",
                    Arguments = $"set wg0 peer {publicKey} allowed-ips 10.200.20.{id}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();
            if (string.IsNullOrEmpty(error)) { return output; }
            else { return error; }
        }

        private string generatePublicKey(string privateKey)
        {
            var process = new Process()
            {
                StartInfo = new()
                {
                    FileName = $"echo {privateKey}",
                    Arguments = $"| wg pubkey",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (string.IsNullOrEmpty(error)) { return output; }
            else { return error; }
        }

        private string generatePrivateKey()
        {
            var process = new Process()
            {
                StartInfo = new()
                {
                    FileName = "wg",
                    Arguments = $"genkey",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (string.IsNullOrEmpty(error)) { return output; }
            else { return error; }
        }

        private string showWireguard()
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
                    CreateNoWindow = true
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (string.IsNullOrEmpty(error)) { return output; }
            else { return error; }
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
                return retartWireguard();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        private static string retartWireguard()
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
                    CreateNoWindow = true
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (string.IsNullOrEmpty(error)) { return output; }
            else { return error; }
        }
    }
}
