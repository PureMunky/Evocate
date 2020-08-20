import { Component } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
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
    editing: false
  };

  constructor(private userService: UserService,
    private route: ActivatedRoute,
    private initService: InitService,
    private topicService: TopicService,
    private mentorshipService: MentorshipService,
    private router: Router) {
    initService.Register(() => {
      this.currentUser = this.userService.CurrentUser();
      this.route.params.subscribe(params => {
        if (params["name"]) {
          this.currentTopic.name = params["name"];
          this.working.editing = true;
        } else if (params["topicId"]) {
          this.loadTopic(params["topicId"]);
          this.working.editing = false;
        }
      })
    });
  }

  public loadTopic(topicId: string) {
    return this.topicService.GetTopic(topicId).then(t => {
      this.currentTopic = t;
      this.working.editing = false;
    });
  }

  public reload() {
    return this.loadTopic(this.currentTopic.topicId);
  }

  public save() {
    // TODO: How does a topic get edited after being created?
    // By the creator? Admins? Mentors?
    // Add a featured flag and admin lock down for support items?
    // Topic link for admin topics that can be edited to link to FAQ/etc.
    this.topicService.Save(this.currentTopic).then(t => {
      this.router.navigate(['/topic/', t.topicId])
    });
  }

  public learn() {
    this.topicService.RegisterLearner(this.currentUser.userId, this.currentTopic.topicId).then(() => this.reload());
  }

  public teach() {
    this.topicService.RegisterMentor(this.currentUser.userId, this.currentTopic.topicId).then(() => this.reload());
  }

  public claimNextMentorship() {
    // TODO: Figure out notifications.
    this.topicService.ClaimNextMentorship(this.currentUser.userId, this.currentTopic.topicId).then(m => {
      this.router.navigate(['/mentorship/', m.mentorshipId]);
    });
  }

}
