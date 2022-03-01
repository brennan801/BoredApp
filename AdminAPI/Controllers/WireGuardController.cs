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
        public ClientInformation Post(ClientMessageInfo clientInfo)
        {
            try
            {
                string privateKey = generatePrivateKey();
                string publicKey = generatePublicKey(privateKey);
                string addPeerResult = addNewPeer(publicKey, clientInfo.Id);
                string restartWireguardResult = restartWireguard();
                ClientInformation newClient = new()
                {
                    ID = clientInfo.Id,
                    ClientName = clientInfo.Name,
                    IpAddress = $"10.200.20.{clientInfo.Id}",
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
            Console.WriteLine(publicKey);
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
            var command = $"echo '{privateKey}' | wg pubkey";
            var process = new Process()
            {
                StartInfo = new()
                {
                    FileName = $"bash",
                    Arguments = "-c \"" + command + "\"",
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
        private static string restartWireguard()
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
                return restartWireguard();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        private static string restartWireguard()
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
