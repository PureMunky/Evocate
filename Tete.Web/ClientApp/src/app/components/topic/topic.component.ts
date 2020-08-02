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
  templateUrl: "./topic.component.html"
})
export class TopicComponent {
  public currentUser: User = new User();
  public currentTopic: Topic = new Topic();
  public topics: Array<Topic> = [];

  public working = {
    displayMentorButtons: true
  };

  constructor(private userService: UserService,
    private route: ActivatedRoute,
    private initService: InitService,
    private topicService: TopicService,
    private mentorshipService: MentorshipService) {
    initService.Register(() => {
      this.currentUser = this.userService.CurrentUser();
      this.route.params.subscribe(params => {
        if (params["name"]) {
          this.currentTopic.name = params["name"];
        } else if (params["topicId"]) {
          this.loadTopic(params["topicId"]);
        }
      })
    });
  }

  public loadTopic(topicId: string) {
    return this.topicService.GetTopic(topicId).then(t => {
      this.currentTopic = t;
    });
  }

  public save() {
    this.topicService.Save(this.currentTopic).then(t => this.loadTopic(t.topicId));
  }

  public learn() {
    this.topicService.RegisterLearner(this.currentUser.userId, this.currentTopic.topicId);
  }

  public teach() {
    console.log('register as a mentor');
  }

}
