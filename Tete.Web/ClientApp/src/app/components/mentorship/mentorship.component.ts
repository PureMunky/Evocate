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
  templateUrl: "./mentorship.component.html"
})
export class MentorshipComponent {
  public currentUser: User = new User();
  public currentMentorship: Mentorship = new Mentorship();

  public working = {
    editing: false,
    contactDetails: '',
    unsavedContact: true,
    closing: false,
    showLearner: false,
    showMentor: false,
    loading: true
  };

  constructor(private userService: UserService,
    private route: ActivatedRoute,
    private initService: InitService,
    private topicService: TopicService,
    private mentorshipService: MentorshipService) {
    initService.Register(() => {
      this.currentUser = this.userService.CurrentUser();
      this.working.contactDetails = this.currentUser.profile.privateAbout;
      this.route.params.subscribe(params => {
        if (params["mentorshipId"]) {
          this.load(params["mentorshipId"]);
        }
      })
    });
  }

  public load(mentorshipId: string) {
    this.working.showLearner = false;
    this.working.showMentor = false;

    return this.mentorshipService.GetMentorship(mentorshipId).then(m => {
      this.currentMentorship = m;
      let contactDetails: string = null;
      if (this.currentMentorship.learnerUserId == this.currentUser.userId) {
        contactDetails == this.currentMentorship.learnerContact;
        this.working.showLearner = true;
      } else if (this.currentMentorship.mentorUserId == this.currentUser.userId) {
        contactDetails = this.currentMentorship.mentorContact;
        this.working.showMentor = true;
      }

      if (contactDetails == null) {
        this.working.unsavedContact = true;
        this.working.contactDetails = this.currentUser.profile.privateAbout;
      } else {
        this.working.unsavedContact = false;
        this.working.contactDetails = contactDetails;
      }

      this.working.editing = false;
      this.working.loading = false;
    });
  }

  public save() {
    return this.mentorshipService.SetContactDetails(this.currentMentorship.mentorshipId, this.currentUser.userId, this.working.contactDetails).then(m => {
      this.currentMentorship = m;
      this.working.unsavedContact = false;
      this.working.editing = false;
    })
  }

  public toggleEdit() {
    this.working.editing = !this.working.editing;
  }

  public beginClose() {
    this.working.closing = true;
  }
  public finishClose() {
    return this.mentorshipService.CloseMentorship(this.currentMentorship).then(m => {
      this.currentMentorship = m;
      this.working.closing = false;
    });
  }

  public cancelClose() {
    this.working.closing = false;
    this.currentMentorship.comments = null;
    this.currentMentorship.rating = null;
  }

  public translateRating(rating: number): string {
    let ratings = ['Blank',
      'Terrible',
      'Bad',
      'Medium',
      'Good',
      'Great'
    ];

    return ratings[rating] || ratings[0];
  }

}
