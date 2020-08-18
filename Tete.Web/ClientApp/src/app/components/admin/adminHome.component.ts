import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { InitService } from "../../services/init.service";
import { UserService } from "../../services/user.service";
import { TopicService } from "../../services/topic.service";
import { MentorshipService } from "../../services/mentorship.service";
import { User } from "../../models/user";
import { Topic } from "../../models/topic";
import { Mentorship } from "../../models/mentorship";

@Component({
  selector: "teteAdminHome",
  templateUrl: "./adminHome.component.html"
})
export class AdminHome {
  public currentUser: User = null;
  public search = {
    done: false,
    text: '',
    newTopic: false
  };
  public tmp = {
    searchText: ''
  };

  public topics: Array<Topic> = [];
  public learningMentorships: Array<Mentorship> = [];
  public teachingMentorships: Array<Mentorship> = [];
  public currentUserTopics: Array<Topic>[] = [];

  constructor(private userService: UserService,
    private initService: InitService,
    private router: Router) {
    initService.Register(() => {
      this.currentUser = userService.CurrentUser();
      // Boot Non-Admins
      if (!this.currentUser.roles.some(r => r == "Admin")) {
        this.router.navigate(['/']);
      }
    });
  }
}
