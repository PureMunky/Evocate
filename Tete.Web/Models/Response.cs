using System;

namespace Tete.Web.Models
{

  public class Response
  {
    /// <summary>
    /// The request that was used to get this response.
    /// </summary>
    /// <value></value>
    public Request Request { get; set; }

    public string Data { get; set; }

    public bool Error { get; set; }

    public string Message { get; set; }

    public System.Net.HttpStatusCode Status {get; set;}

  }
}