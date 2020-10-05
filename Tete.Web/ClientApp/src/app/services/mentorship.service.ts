import { Injectable, Inject } from "@angular/core";
import { ApiService } from "./api.service";
import { Mentorship } from "../models/mentorship";
import { Evaluation } from "../models/evaluation";

@Injectable({
  providedIn: "root"
})
export class MentorshipService {
  private UrlSaveMentorship: string = '/V1/Mentorship/Post';
  private UrlGetUserMentorships = (userId: string) => `/V1/Mentorship/GetUserMentorships?UserId=${userId}`;
  private UrlGetMentorship = (mentorshipId: string) => `/V1/Mentorship/GetMentorship?MentorshipId=${mentorshipId}`;
  private UrlSetContactDetails = "/V1/Mentorship/SetContactDetails";
  private UrlCloseMentorship = "/V1/Mentorship/CloseMentorship";
  private UrlCancelMentorship = (mentorshipId: string) => `/V1/Mentorship/CancelMentorship?MentorshipId=${mentorshipId}`;

  constructor(private apiService: ApiService) {

  }

  public Save(mentorship: Mentorship): Promise<any> {
    return this.apiService.post(this.UrlSaveMentorship, mentorship, [
      this.UrlGetMentorship(mentorship.mentorshipId)
    ]).then(t => {
      return t[0];
    });
  }

  public GetUserMentorships(userId: string) {
    return this.apiService.get(this.UrlGetUserMentorships(userId)).then(t => {
      return t;
    });
  }

  public GetMentorship(mentorshipId: string) {
    return this.apiService.get(this.UrlGetMentorship(mentorshipId), 100000).then(m => {
      return m[0];
    });
  }

  public SetContactDetails(mentorshipId: string, userId: string, contactDetails: string) {
    return this.apiService.post(this.UrlSetContactDetails, {
      mentorshipId: mentorshipId,
      userId: userId,
      ContactDetails: contactDetails
    }, [
      this.UrlGetMentorship(mentorshipId)
    ]).then(m => {
      return m[0];
    });
  }

  public CloseMentorship(evaluation: Evaluation) {
    return this.apiService.post(this.UrlCloseMentorship, evaluation, [
      this.UrlGetMentorship(evaluation.mentorshipId)
    ]).then(m => {
      return m[0];
    });
  }

  public CancelMentorship(mentorshipId: string) {
    return this.apiService.post(this.UrlCancelMentorship(mentorshipId), {}, [
      this.UrlGetMentorship(mentorshipId)
    ]).then(m => {
      return m[0];
    });
  }

}
