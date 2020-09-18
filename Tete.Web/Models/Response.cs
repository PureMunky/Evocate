using System.Collections.Generic;
using System.Net;

namespace Tete.Web.Models
{

  public class Response<T>
  {
    /// <summary>
    /// The request that was used to get this response.
    /// </summary>
    /// <value></value>
    public Request Request { get; set; }

    public IEnumerable<T> Data { get; set; }

    public bool Error { get; set; }

    public string Message { get; set; }

    public HttpStatusCode Status { get; set; }

    public Response(T item)
    {
      this.Data = new List<T>() { item };
      this.Status = HttpStatusCode.OK;
      this.Error = false;
    }

    public Response(IEnumerable<T> items)
    {
      this.Data = items;
      this.Status = HttpStatusCode.OK;
      this.Error = false;
    }

    public Response(T item, bool error)
    {
      this.Data = new List<T>() { item };
      this.Status = (error ? HttpStatusCode.BadRequest : HttpStatusCode.OK);
      this.Error = error;
    }

    public Response(IEnumerable<T> items, bool error)
    {
      this.Data = items;
      this.Status = (error ? HttpStatusCode.BadRequest : HttpStatusCode.OK);
      this.Error = error;
    }
  }
}