import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { InitService } from "../../services/init.service";
import { UserService } from "../../services/user.service";
import { TopicService } from "../../services/topic.service";
import { User } from "../../models/user";
import { Topic } from "../../models/topic";

@Component({
  selector: "topic",
  templateUrl: "./topic.component.html"
})
export class TopicComponent {
  public currentUser: User = new User();
  public currentTopic: Topic = new Topic();
  public topics: Array<Topic> = [];

  constructor(private userService: UserService,
    private route: ActivatedRoute,
    private initService: InitService,
    private topicService: TopicService) {
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

}
