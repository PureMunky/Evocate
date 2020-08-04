import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { InitService } from "../../services/init.service";
import { UserService } from "../../services/user.service";
import { TopicService } from "../../services/topic.service";
import { MentorshipService } from "../../services/mentorship.service";
import { User } from "../../models/user";
import { Topic } from "../../models/topic";
import { Mentorship } from "../../models/mentorship";

@Component({
  selector: "topic",
  templateUrl: "./mentorship.component.html"
})
export class MentorshipComponent {
  public currentUser: User = new User();
  public currentMentorship: Mentorship = new Mentorship();

  public working = {
    editing: true
  };

  constructor(private userService: UserService,
    private route: ActivatedRoute,
    private initService: InitService,
    private topicService: TopicService,
    private mentorshipService: MentorshipService) {
    initService.Register(() => {
      this.currentUser = this.userService.CurrentUser();
      this.route.params.subscribe(params => {
        if (params["mentorshipId"]) {
          this.load(params["mentorshipId"]);
        }
      })
    });
  }

  public load(mentorshipId: string) {
    return this.mentorshipService.GetMentorship(mentorshipId).then(m => {
      this.currentMentorship = m;
      this.working.editing = false;
    });
  }

  public save() {
    // this.topicService.Save(this.currentTopic).then(t => this.loadTopic(t.topicId));
  }

}
