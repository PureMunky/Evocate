using System;
using System.ComponentModel.DataAnnotations;

namespace Tete.Models.Users
{

  public class Evaluation
  {
    [Required]
    public Guid EvaluationId { get; set; }

    [Required]
    public Guid MentorshipId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    public string Comments { get; set; }

    [Required]
    public EvaluationUserType UserType { get; set; }

    public Evaluation()
    {
      this.EvaluationId = Guid.NewGuid();
    }

    public Evaluation(Guid MentorshipId, Guid UserId, EvaluationUserType UserType)
    {
      this.EvaluationId = Guid.NewGuid();
      this.MentorshipId = MentorshipId;
      this.UserId = UserId;
      this.CreatedDate = DateTime.UtcNow;
      this.UserType = UserType;
    }
  }

  public enum EvaluationUserType
  {
    Learner = 0,
    Mentor = 1
  }
}