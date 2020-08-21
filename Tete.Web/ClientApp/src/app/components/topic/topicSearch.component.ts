import { Component } from "@angular/core";
import { InitService } from "../../services/init.service";
import { UserService } from "../../services/user.service";
import { TopicService } from "../../services/topic.service";
import { User } from "../../models/user";
import { Topic } from "../../models/topic";

@Component({
  selector: "teteTopicSearch",
  templateUrl: "./topicSearch.component.html"
})
export class TopicSearchComponent {
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

  constructor(private userService: UserService,
    private initService: InitService,
    private topicService: TopicService) {
    initService.Register(() => {
      this.currentUser = userService.CurrentUser();
    });
  }

  public Search() {
    this.search.done = false;
    this.topicService.Search(this.tmp.searchText).then(d => {
      this.topics = d;
      this.search.done = true;
      this.search.text = this.tmp.searchText;
      this.search.newTopic = !this.topics.some(t => t.name == this.search.text);
    });
  }
}
