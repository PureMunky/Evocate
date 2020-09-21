import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { ApiService } from "../../services/api.service";
import { UserService } from "../../services/user.service";
import { InitService } from "../../services/init.service";
import { LanguageService } from "../../services/language.service";
import { User } from "../../models/user";


@Component({
  selector: "profile",
  templateUrl: "./profile.component.html"
})
export class ProfileComponent {
  public user: User = new User(null);
  public currentUser: User = new User(null);
  public languages = [];
  public tmpModel = {
    language: ''
  };
  public working: Working = new Working();

  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService,
    private userService: UserService,
    private initService: InitService,
    private languageService: LanguageService
  ) {
    this.initService.Register(() => this.load());
  }

  private load() {
    this.working = new Working();
    this.currentUser = this.userService.CurrentUser();
    this.route.params.subscribe(params => {
      this.working.userName = params["username"];
      if (this.working.userName) {
        this.loadUser();
        this.working.self = (this.working.userName == this.currentUser.userName);
      } else {
        this.user = JSON.parse(JSON.stringify(this.currentUser));
        this.working.self = true;
        this.working.editing = true;
      }
    });

    this.languages = this.languageService.Languages();
  }
  private loadUser() {
    return this.userService.Get(this.working.userName).then(u => {
      this.user = u;
    });
  }
  public save() {
    this.apiService.post('/V1/User/Post', this.user).then(u => {
      this.working.editing = false;
    });
  }

  public cancel() {
    this.user = JSON.parse(JSON.stringify(this.currentUser));
    this.working.editing = false;
  }

  public addLanguage() {
    var selectedLanguage = this.languages.filter(l => l.languageId == this.tmpModel.language)[0];

    var exists = this.user.languages.some(l => l.languageId == selectedLanguage.languageId);

    if (!exists) {
      this.user.languages.push(selectedLanguage);
    }
  }

  public getLangName(langId) {
    return this.languages.filter(l => l.languageId == langId)[0].name;
  }

  public removeLanguage(langId) {
    this.user.languages = this.user.languages.filter(l => l.languageId != langId);
  }

}

class Working {
  public editing: boolean;
  public self: boolean;
  public error: boolean;
  public errorMessage: string;
  public userName: string;
  public responseMessages: Array<string>;

  constructor() {
    this.editing = false;
    this.self = false;
    this.error = false;
    this.errorMessage = '';
    this.userName = '';
    this.responseMessages = [];
  }
};


