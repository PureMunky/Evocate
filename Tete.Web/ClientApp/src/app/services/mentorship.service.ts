import { Injectable, Inject } from "@angular/core";
import { ApiService } from "./api.service";
import { Mentorship } from "../models/mentorship";

@Injectable({
  providedIn: "root"
})
export class MentorshipService {
  constructor(private apiService: ApiService) {

  }

  public Save(mentorship: Mentorship): Promise<any> {
    return this.apiService.post("/V1/Mentorship/Post", mentorship).then(t => {
      return t[0];
    });
  }

  public GetUserMentorships(userId: string) {
    return this.apiService.get("/V1/Mentorship/GetUserMentorships?UserId=" + userId).then(t => {
      return t;
    });
  }

}
