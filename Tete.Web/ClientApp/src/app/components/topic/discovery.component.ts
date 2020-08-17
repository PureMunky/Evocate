import { Component } from "@angular/core";
import { InitService } from "../../services/init.service";
import { UserService } from "../../services/user.service";
import { TopicService } from "../../services/topic.service";
import { User } from "../../models/user";
import { Topic } from "../../models/topic";

@Component({
  selector: "teteTopicDiscovery",
  templateUrl: "./discovery.component.html"
})
export class TopicDiscoveryComponent {
  public currentUser: User = null;

  public topTopics: Array<Topic> = [];
  public newestTopics: Array<Topic> = [];
  public waitingTopics: Array<Topic> = [];

  constructor(private userService: UserService,
    private initService: InitService,
    private topicService: TopicService) {
    initService.Register(() => {
      this.currentUser = userService.CurrentUser();
      this.topicService.GetTopTopics().then(topics => {
        this.topTopics = topics;
      });
      this.topicService.GetNewestTopics().then(topics => {
        this.newestTopics = topics;
      });
      this.topicService.GetWaitingTopics().then(topics => {
        this.waitingTopics = topics;
      });
    });
  }

}
