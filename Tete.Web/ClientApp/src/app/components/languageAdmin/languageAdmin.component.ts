import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { UserService } from "../../services/user.service";

@Component({
  selector: "language-admin",
  templateUrl: "./languageAdmin.component.html"
})
export class LanguageAdminComponent {
  public Languages = [
    { LanguageId: "", name: "none", active: false, elements: [], elems: {} }
  ];
  public newLanguageInput = "";
  public currentLanguage = this.Languages[0];
  public fullElements = [];
  public newLanguage = { LanguageId: "", name: "none", active: false, elements: [], elems: {} };

  public addLanguage = function () {
    this.Languages.push({
      LanguageId: "",
      name: "new",
      active: true,
      elements: [],
      elems: {}
    });
    // this.newLanguage.name = newName;
    // this.newLanguage.active = true;
    // return this.apiService
    //   .post("V1/Languages/Post",
    //     this.newLanguage
    //   )
    //   .then(this.loadLanguages);
  };

  public loadLanguages = async function () {
    var result = this.apiService.get("V1/Languages/Get")
      .then(result => {
        this.Languages = result.map(this.processLanguage);

        if (this.Languages.length > 0) {
          this.currentLanguage = this.Languages[0];
        }

        this.apiService.get("V1/Languages/New")
          .then(lang => {
            this.newLanguage = lang;
            console.log(this.newLanguage);
          });
      });
  };

  public addElement() {
    if (!this.currentLanguage.elems) {
      this.currentLanguage.elems = {};
    }

    this.fullElements.push({key: "new"});
  }

  private processLanguage(language) {
    let rtnLang = language;

    rtnLang.elems = {};

    return rtnLang;
  }

  constructor(
    private apiService: ApiService,
    private userService: UserService
  ) {
    this.loadLanguages();
  }
}
