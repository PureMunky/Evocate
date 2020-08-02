import { Component } from "@angular/core";
import { InitService } from "../services/init.service";
import { UserService } from "../services/user.service";
import { TopicService } from "../services/topic.service";
import { MentorshipService } from "../services/mentorship.service";
import { Topic } from "../models/topic";
import { Mentorship } from "../models/mentorship";

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
  public mentorships: Array<Mentorship> = [];

  constructor(private userService: UserService,
    private initService: InitService,
    private topicService: TopicService,
    private mentorshipService: MentorshipService) {
    initService.Register(() => {
      this.userName = userService.CurrentUser().displayName;
      mentorshipService.GetUserMentorships(userService.CurrentUser().userId).then(m => {
        this.mentorships = m;
      })
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
