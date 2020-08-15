export class Mentorship {
  public mentorshipId: string;
  public learnerUserId: string;
  public learnerContact: string;
  public mentorUserId: string;
  public mentorContact: string;
  public topicId: string;
  public active: boolean;
  public createdDate: Date;
  public startDate: Date;
  public endDate: Date;

  public learnerClosed: boolean = false;
  public learnerClosedDate: Date;
  public learnerRating: number;
  public learnerClosingComments: string;

  public mentorClosed: boolean = false;
  public mentorClosedDate: Date;
  public mentorRating: number;
  public mentorClosingComments: string;
}