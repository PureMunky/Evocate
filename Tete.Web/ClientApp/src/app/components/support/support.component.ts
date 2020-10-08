import { Component } from "@angular/core";
import { UserService } from "../../services/user.service";
import { InitService } from "../../services/init.service";
import { TopicService } from "../../services/topic.service";
import { User } from "../../models/user";
import { Topic } from "../../models/topic";


@Component({
  selector: "support",
  templateUrl: "./support.component.html"
})
export class SupportComponent {
  public currentUser: User = new User(null);
  public topics: Array<Topic> = [];

  constructor(
    private userService: UserService,
    private initService: InitService,
    private topicService: TopicService
  ) {
    this.initService.Register(() => {
      this.currentUser = this.userService.CurrentUser();

      topicService.GetKeywordTopics("support").then(topics => {
        this.topics = topics;
      })
    });
  }

} 