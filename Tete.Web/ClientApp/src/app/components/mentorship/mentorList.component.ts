import { Component, Input } from '@angular/core';
import { InitService } from "../../services/init.service";
import { UserService } from "../../services/user.service";
import { Mentorship } from '../../models/mentorship';
import { User } from '../../models/user';

@Component({
  selector: "teteMentorList",
  templateUrl: "./mentorList.component.html"
})
export class MentorList {
  @Input('mentorships') mentorships: Array<Mentorship>;
  public currentUser: User = null;

  constructor(private userService: UserService,
    private initService: InitService) {
    initService.Register(() => {
      this.currentUser = userService.CurrentUser();
    });
  }
}