import { Component } from "@angular/core";
import { InitService } from "../services/init.service";
import { UserService } from "../services/user.service";
import { TopicService } from "../services/topic.service";
import { MentorshipService } from "../services/mentorship.service";
import { User } from "../models/user";
import { Topic } from "../models/topic";
import { Mentorship } from "../models/mentorship";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html"
})
export class HomeComponent {
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
    private topicService: TopicService,
    private mentorshipService: MentorshipService) {
    initService.Register(() => {
      this.currentUser = userService.CurrentUser();
      mentorshipService.GetUserMentorships(userService.CurrentUser().userId).then(m => {
        this.teachingMentorships = m.filter(m => m.mentorUserId == this.currentUser.userId);
        this.learningMentorships = m.filter(m => m.learnerUserId == this.currentUser.userId);
      });
      topicService.GetUserTopics(this.currentUser.userId).then(topics => {
        this.currentUserTopics = topics;
      });
    });
  }
}
