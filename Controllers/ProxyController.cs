using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AspNetCore.Proxy;
using AspNetCore.Proxy.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ProxyExample.Controllers
{
    public class ProxyController : Controller
    {
        private static IConfiguration _configuration;

        public ProxyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("/{rest}")]
        public Task ProxyCatchAll(string rest)
        {
            //to replace wrong encoding from swagger ui parameters
            var encodedRest = Regex.Replace(rest, @"%2F", "/");

            return this.HttpProxyAsync($"{_configuration["URL"]}/{encodedRest}");
        }
    }
}
