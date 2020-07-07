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
    editing: false
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
        if (params["username"] != this.currentUser.userName) {
          userService.Get(params["username"]).then(u => {
            this.user = u;
          });
          this.working.editing = false;
        } else {
          this.user = userService.CurrentUser();
          this.working.editing = true;
        }
      });

      this.languages = this.languageService.Languages();
    });
  }

  public save() {
    this.apiService.post('/V1/Profile/Post', this.user.profile);
  }

  public addLanguage() {
    var selectedLanguage = this.languages.filter(l => l.languageId == this.tmpModel.language)[0];

    var exists = this.user.languages.some(l => l.languageId == selectedLanguage.languageId);

    if (!exists) {
      this.user.languages.push(selectedLanguage);
    }

  }
}