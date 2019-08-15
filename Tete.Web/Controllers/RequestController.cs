using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using Tete.Web.Models;

namespace Tete.Web.Controllers
{
  [Route("api/[controller]")]
  public class RequestController : Controller
  {

    private IConfiguration Configuration;
    public RequestController(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    [HttpGet]
    public Request Get()
    {
      return new Request();
    }

    [HttpGet("{id}")]
    public async Task<HttpResponseMessage> Get(string id)
    {
      HttpResponseMessage response;

      using (var client = new HttpClient())
      {
        response = await client.GetAsync(Path.Join(Configuration["Tete:ApiEndpoint"], "v1/Flags"));
      }

      return response;
    }

    [HttpPost]
    public async Task<HttpResponseMessage> Post([FromBody] Request value)
    {
      HttpResponseMessage response;

      using (var client = new HttpClient())
      {
        response = await client.GetAsync(Path.Join(Configuration["Tete:ApiEndpoint"], "v1/Flags"));
      }

      return response;
    }

  }
}
