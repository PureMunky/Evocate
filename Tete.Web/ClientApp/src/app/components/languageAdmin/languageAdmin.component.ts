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

  public addLanguage = function (newLanguage: String) {
    return this.apiService
      .get({
        url: "/v1/Languages",
        method: "Post",
        body: newLanguage
      })
      .then(() => this.loadLanguages());
  };

  public loadLanguages = function () {
    return this.apiService
      .get({
        url: "/v1/Languages",
        method: "Get",
        body: ""
      })
      .then(result => {
        this.Languages = result;

        this.Languages = this.Languages.map(l => {
          return l;
        });

        if (this.Languages.length > 0) {
          this.currentLanguage = this.Languages[0];
        }
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
