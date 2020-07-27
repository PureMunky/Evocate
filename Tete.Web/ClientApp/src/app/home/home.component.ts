import { Component } from "@angular/core";
import { InitService } from "../services/init.service";
import { UserService } from "../services/user.service";
import { TopicService } from "../services/topic.service";
import { Topic } from "../models/topic";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html"
})
export class HomeComponent {
  public userName = 'world';
  public search = {
    done: false,
    text: ''
  };
  public tmp = {
    searchText: ''
  };

  public topics: Array<Topic> = [];

  constructor(private userService: UserService,
    private initService: InitService,
    private topicService: TopicService) {
    initService.Register(() => {
      this.userName = userService.CurrentUser().displayName;
    });
  }

  public Search() {
    this.search.done = false;
    this.topicService.Search(this.tmp.searchText).then(d => {
      this.topics = d;
      this.search.done = true;
      this.search.text = this.tmp.searchText;
    });
  }
}
