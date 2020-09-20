import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { InitService } from "../../services/init.service";
import { UserService } from "../../services/user.service";
import { TopicService } from "../../services/topic.service";
import { MentorshipService } from "../../services/mentorship.service";
import { User } from "../../models/user";
import { Topic } from "../../models/topic";
import { Mentorship } from "../../models/mentorship";
import { Evaluation } from "../../models/evaluation";

@Component({
  selector: "topic",
  templateUrl: "./mentorship.component.html"
})
export class MentorshipComponent {
  public currentUser: User = new User(null);
  public currentMentorship: Mentorship = new Mentorship();

  public working = {
    editing: false,
    contactDetails: '',
    unsavedContact: true,
    closing: false,
    closed: false,
    showLearner: false,
    showMentor: false,
    loading: true,
    evaluation: new Evaluation(),
    otherPerson: new User(null)
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

    return this.mentorshipService.GetMentorship(mentorshipId).then(m => this.processMentorship(m));
  }

  public save() {
    return this.mentorshipService.SetContactDetails(this.currentMentorship.mentorshipId, this.currentUser.userId, this.working.contactDetails).then(m => this.processMentorship(m));
  }

  public toggleEdit() {
    this.working.editing = !this.working.editing;
  }

  public beginClose() {
    this.working.closing = true;
  }

  public finishClose() {
    if (this.currentMentorship.hasMentor) {
      this.working.evaluation.mentorshipId = this.currentMentorship.mentorshipId;
      return this.mentorshipService.CloseMentorship(this.working.evaluation).then(m => this.processMentorship(m));
    } else {
      return this.mentorshipService.CancelMentorship(this.currentMentorship.mentorshipId).then(m => this.processMentorship(m));
    }
  }

  public cancelClose() {
    this.working.closing = false;
    this.working.evaluation.comments = null;
    this.working.evaluation.rating = null;
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

  private processMentorship(mentorship) {
    this.currentMentorship = mentorship;
    var contactDetails: string = null;

    if (this.currentMentorship.learnerUserId == this.currentUser.userId) {
      contactDetails = this.currentMentorship.learnerContact;
      this.working.showLearner = true;
      this.working.otherPerson = this.currentMentorship.mentor;
      this.working.closed = this.currentMentorship.learnerClosed;
    } else if (this.currentMentorship.mentorUserId == this.currentUser.userId) {
      contactDetails = this.currentMentorship.mentorContact;
      this.working.showMentor = true;
      this.working.otherPerson = this.currentMentorship.learner;
      this.working.closed = this.currentMentorship.mentorClosed;
    }

    if (contactDetails == null || contactDetails.length == 0) {
      this.working.unsavedContact = true;
      this.working.contactDetails = this.currentUser.profile.privateAbout;
    } else {
      this.working.unsavedContact = false;
      this.working.contactDetails = contactDetails;
    }

    if (!this.currentMentorship.active) {
      this.working.closed = true;
    }

    this.working.editing = false;
    this.working.loading = false;
  }

}
