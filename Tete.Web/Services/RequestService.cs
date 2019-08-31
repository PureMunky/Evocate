using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Tete.Web.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tete.Web.Services
{

  public class RequestService<T>
  {
    IConfiguration Configuration;

    public RequestService(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public async Task<dynamic> Get(string route, HttpContext context)
    {
      dynamic result;
      var cookieContainer = new CookieContainer();

      using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
      {
        using (var client = new HttpClient(handler))
        {
          try
          {
            Uri Url = new Uri(Configuration["Tete:ApiEndpoint"] + route);
            cookieContainer.Add(Url, new Cookie(Constants.SessionTokenName, context.Request.Cookies[Constants.SessionTokenName]));

            HttpResponseMessage res = await client.GetAsync(Url);
            string content = await res.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject(content);
          }
          catch (Exception)
          {
            result = null;
          }
        }

        if (result == null)
        {
          context.Response.StatusCode = 401;
        }
        else
        {
          context.Response.StatusCode = 200;
        }

        return result;
      }
    }
  }
}