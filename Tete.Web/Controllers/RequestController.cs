using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public Response Get()
    {
      return new Response();
    }

    [HttpGet("{id}")]
    public async Task<Response> Get(string id)
    {
      Request request = new Request()
      {
        Url = Configuration["Tete:ApiEndpoint"] + "/v1/Flags",
        Method = "Post",
        Body = String.Empty
      };
      Response response = new Response()
      {
        Request = request
      };

      using (var client = new HttpClient())
      {
        try
        {
          HttpResponseMessage res = await client.GetAsync(request.Url);
          response.Data = await res.Content.ReadAsStringAsync();
          response.Status = res.StatusCode;
        }
        catch (Exception e)
        {
          response.Error = true;
          response.Message = e.Message;
        }
      }

      return response;
    }

    [HttpPost]
    public async Task<HttpResponseMessage> Post([FromBody] Request value)
    {
      HttpResponseMessage response;

      using (var client = new HttpClient())
      {
        response = await client.GetAsync(Configuration["Tete:ApiEndpoint"] + "/v1/Flags");
      }

      return response;
    }

  }
}
