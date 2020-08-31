import { Component } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { InitService } from "../../services/init.service";
import { UserService } from "../../services/user.service";
import { TopicService } from "../../services/topic.service";
import { MentorshipService } from "../../services/mentorship.service";
import { User } from "../../models/user";
import { Topic } from "../../models/topic";
import { Mentorship } from "../../models/mentorship";
import { Keyword } from "../../models/keyword";
import { Link } from "../../models/link";

@Component({
  selector: "topic",
  templateUrl: "./topic.component.html"
})
export class TopicComponent {
  public currentUser: User = new User();
  public currentTopic: Topic = new Topic();
  public topics: Array<Topic> = [];
  public adminUser: boolean = false;

  public working = {
    editing: false,
    keywordName: '',
    linkName: '',
    linkDestination: '',
    creating: false
  };

  constructor(private userService: UserService,
    private route: ActivatedRoute,
    private initService: InitService,
    private topicService: TopicService,
    private mentorshipService: MentorshipService,
    private router: Router) {
    initService.Register(() => {
      this.currentUser = this.userService.CurrentUser();
      this.adminUser = this.currentUser.roles.some(r => r == "Admin");

      this.route.params.subscribe(params => {
        if (params["name"]) {
          this.currentTopic.name = params["name"];
          this.working.editing = true;
          this.working.creating = true;
        } else if (params["topicId"]) {
          this.loadTopic(params["topicId"]);
          this.working.editing = false;
          this.working.creating = false;
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
    this.topicService.Save(this.currentTopic).then(t => {
      if (this.working.creating) {
        this.router.navigate(['/topic/', t.topicId])
      } else {
        this.reload();
      }
    });
  }

  public addKeyword() {
    if (this.working.keywordName.length > 0) {
      var newKeyword = new Keyword();
      newKeyword.name = this.working.keywordName;
      this.currentTopic.keywords.push(newKeyword);
      this.working.keywordName = '';
    }
  }

  public removeKeyword(keyword: Keyword) {
    this.currentTopic.keywords = this.currentTopic.keywords.filter(k => k.name != keyword.name);
  }

  public addLink() {
    if (this.working.linkName.length > 0 && this.working.linkDestination.length > 0) {
      var newLink = new Link();
      newLink.name = this.working.linkName;
      newLink.destination = this.working.linkDestination;
      this.currentTopic.links.push(newLink);
      this.working.linkName = '';
      this.working.linkDestination = '';
    }
  }

  public removeLink(link: Link) {
    this.currentTopic.links = this.currentTopic.links.filter(l => l.destination != link.destination);
  }

  public learn() {
    // TODO: Work through how to request a mentor for a topic you've already been mentored in.
    this.topicService.RegisterLearner(this.currentUser.userId, this.currentTopic.topicId).then(() => this.reload());
  }

  public teach() {
    this.topicService.RegisterMentor(this.currentUser.userId, this.currentTopic.topicId).then(() => this.reload());
  }

  public claimNextMentorship() {
    // TODO: Figure out notifications/notify the learner that they've been picked up.
    this.topicService.ClaimNextMentorship(this.currentUser.userId, this.currentTopic.topicId).then(m => {
      this.router.navigate(['/mentorship/', m.mentorshipId]);
    });
  }

}
