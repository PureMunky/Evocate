using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Tete.Web.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tete.Web.Services
{

  public class RequestService
  {
    IConfiguration Configuration;

    public RequestService(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public async Task<Y> Post<T, Y>(string route, T body, HttpContext context)
    {
      Y result = default(Y);
      var cookieContainer = new CookieContainer();
      context.Response.StatusCode = 401;

      using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
      {
        using (var client = new HttpClient(handler))
        {
          try
          {
            Uri Url = new Uri(Configuration["Tete:ApiEndpoint"] + route);
            cookieContainer.Add(Url, new Cookie(Constants.SessionTokenName, context.Request.Cookies[Constants.SessionTokenName]));

            HttpResponseMessage res = await client.PostAsJsonAsync<T>(Url, body);
            string content = await res.Content.ReadAsStringAsync();
            result = ((JObject)JsonConvert.DeserializeObject(content)).ToObject<Y>();
          }
          catch (Exception)
          {
            result = default(Y);
          }
        }

        if (result != null)
        {
          context.Response.StatusCode = 200;
        }
      }
      return result;
    }

    public async Task<dynamic> Get(string route, HttpContext context)
    {
      dynamic result = null;
      var cookieContainer = new CookieContainer();
      context.Response.StatusCode = 401;

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

        if (result != null)
        {
          context.Response.StatusCode = 200;
        }
      }
      return result;
    }
  }
}