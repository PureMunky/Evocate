import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { UserService } from "../../services/user.service";
import { InitService } from "../../services/init.service";
import { LanguageService } from "../../services/language.service";

@Component({
  selector: "profile",
  templateUrl: "./profile.component.html"
})
export class ProfileComponent {
  public user = {
    displayName: '',
    profile: {
      about: ''
    },
    languages: []
  };
  public languages = [];
  public tmpModel = {
    language: ''
  };

  constructor(
    private apiService: ApiService,
    private userService: UserService,
    private initService: InitService,
    private languageService: LanguageService
  ) {
    initService.Register(() => {
      this.user = userService.CurrentUser();
      this.languages = languageService.Languages();
    });
  }

  public save() {
    console.log(this.user);
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