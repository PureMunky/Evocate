using System;

namespace Tete.Models.Logging
{

  public class Log
  {
    public Guid LogId { get; set; }

    public DateTime Occured { get; set; }

    public string Description { get; set; }

    public string MachineName { get; set; }

    public string Data { get; set; }

    public string Domain { get; set; }

    public string StackTrace { get; set; }

    public Log()
    {
      Init();
    }

    public Log(string Description, string Data = "", string Domain = "")
    {
      Init();
      this.Description = Description;
      this.Data = Data;
      this.Domain = Domain;
    }

    private void Init()
    {
      this.LogId = Guid.NewGuid();
      this.Occured = DateTime.UtcNow;
      this.MachineName = Environment.MachineName;
      this.StackTrace = Environment.StackTrace;
    }

  }

}