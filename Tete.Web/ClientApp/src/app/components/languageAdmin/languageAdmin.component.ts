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
  public newLanguage;

  public addLanguage = function (newName: String) {
    this.newLanguage.name = newName;
    this.newLanguage.active = true;
    return this.apiService
      .get({
        url: "/v1/Languages/Post",
        method: "Post",
        body: JSON.stringify(this.newLanguage)
      })
      .then(() => this.loadLanguages());
  };

  public loadLanguages = async function () {
    var result = this.apiService.get({
      url: "/v1/Languages/Get",
      method: "Get",
      body: ""
    }).then(result => {
      this.Languages = result;
      if (this.Languages.length > 0) {
        this.currentLanguage = this.Languages[0];
      }
      
      this.apiService.get({
        url: "/v1/Languages/New",
        method: "Get",
        body: ""
      }).then(lang => {
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
