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
  public user: User = new User();
  public currentUser: User = new User();
  public languages = [];
  public tmpModel = {
    language: ''
  };
  public working = {
    editing: false,
    self: false,
    error: false,
    errorMessage: '',
    userName: '',
    newPassword: ''
  };

  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService,
    private userService: UserService,
    private initService: InitService,
    private languageService: LanguageService
  ) {
    this.initService.Register(() => {
      this.currentUser = this.userService.CurrentUser();
      this.route.params.subscribe(params => {
        this.working.userName = params["username"];
        this.loadUser();
        if (this.working.userName != this.currentUser.userName) {
          this.working.self = false;
        } else {
          this.working.self = true;
        }
      });

      this.languages = this.languageService.Languages();
    });
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
    this.loadUser().then(x => {
      this.working.editing = false;
    });
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

  public resetPassword() {
    // TODO: make this more secure.
    this.apiService.post('/Login/Reset?newPassword=' + this.working.newPassword, {}).then(u => {
      this.working.editing = false;
    });
  }
}