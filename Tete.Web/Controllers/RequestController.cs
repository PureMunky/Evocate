using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Tete.Web.Models;
using Newtonsoft.Json;

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
          response.Data = JsonConvert.DeserializeObject<dynamic>(await res.Content.ReadAsStringAsync());
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
    public async Task<Response> Post([FromBody] Request request)
    {
      Response response = new Response()
      {
        Request = request
      };


      if (request.Method.ToLower() == "get")
      {
        response.Data = await new Tete.Web.Services.RequestService(Configuration).Get(request.Url, HttpContext);
      }
      else if (request.Method.ToLower() == "post")
      {
        response = await new Tete.Web.Services.RequestService(Configuration).Post<string, Response>(request.Url, request.Body, HttpContext);
      }
      else
      {
        response.Error = true;
        response.Message = "Unhandled method type. Not Implemented.";
      }

      return response;
    }

  }
}
