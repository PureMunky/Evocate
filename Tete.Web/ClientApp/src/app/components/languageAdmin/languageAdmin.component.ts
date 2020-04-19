import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { UserService } from "../../services/user.service";

@Component({
  selector: "language-admin",
  templateUrl: "./languageAdmin.component.html"
})
export class LanguageAdminComponent {
  public Languages = [
    { LanguageId: "", name: "none", active: false, elements: {} }
  ];
  public newLanguageInput = "";
  public currentLanguage = this.Languages[0];
  public fullElements = [
    "greeting",
    "submit",
    "hello",
    "title",
    "home",
    "logging",
    "logout"
  ];
  public newLanguage = { LanguageId: "", name: "none", active: false, elements: {} };

  public addLanguage = function (newName: String) {
    this.newLanguage.name = newName;
    this.newLanguage.active = true;
    return this.apiService
      .post("V1/Languages/Post",
        this.newLanguage
      )
      .then(() => this.loadLanguages());
  };

  public loadLanguages = async function () {
    var result = this.apiService.get("V1/Languages/Get")
      .then(result => {
        this.Languages = result;
        if (this.Languages.length > 0) {
          this.currentLanguage = this.Languages[0];
        }

        this.apiService.get("V1/Languages/New")
          .then(lang => {
            this.newLanguage = lang;
          });
      });

  };

  public addElement() {
    if (!this.currentLanguage.elements) {
      this.currentLanguage.elements = {};
    }
    this.currentLanguage.elements["new"] = "";
  }

  constructor(
    private apiService: ApiService,
    private userService: UserService
  ) {
    this.loadLanguages();
  }
}
