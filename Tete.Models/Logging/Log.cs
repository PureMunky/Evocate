using System;

namespace Tete.Models.Logging
{

  public class Log
  {
    public DateTime Occured { get; set; }

    public string Description { get; set; }

    public string MachineName { get; set; }

    public string StackTrace { get; set; }

    public Log()
    {
      Init();
    }

    public Log(string Description)
    {
      Init();
      this.Description = Description;
    }

    private void Init()
    {
      this.Occured = DateTime.UtcNow;
      this.MachineName = Environment.MachineName;
      this.StackTrace = Environment.StackTrace;
    }

  }

}