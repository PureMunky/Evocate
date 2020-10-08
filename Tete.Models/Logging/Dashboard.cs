namespace Tete.Models.Logging
{

  public class Dashboard
  {

    public int TotalUsers { get; set; } = 0;
    public int RegisteredUsers { get; set; } = 0;
    public int ActiveUsers { get; set; } = 0;

    public int TotalTopics { get; set; } = 0;
    public int ActiveTopics { get; set; } = 0;
    public int RecentTopics { get; set; } = 0;

    public int TotalMentorships { get; set; } = 0;
    public int WaitingMentorships { get; set; } = 0;
    public int ActiveMentorships { get; set; } = 0;
    public int CompletedMentorships { get; set; } = 0;
    public int CancelledMentorships { get; set; } = 0;

    public Dashboard()
    {
    }

  }

}