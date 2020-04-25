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
  };

  public saveLanguage = function (language) {
    var lang = this.prepareForSave(language);
    var rtnPromise;

    if(lang.languageId == null) {
      rtnPromise = this.apiService.post("V1/Languages/Post", lang);
    } else {
      rtnPromise = this.apiService.put("V1/Languages/Update", lang);
    }

    return rtnPromise.then(this.loadLanguages);
  };

  public loadLanguages () {
    var result = this.apiService.get("V1/Languages/Get")
      .then(result => {
        this.Languages = result.map(l => this.processLanguage(l));

        this.apiService.get("V1/Languages/New")
          .then(lang => {
            this.newLanguage = lang;
            console.log(this.newLanguage);
          });
      });
  };

  public addElement() {
    this.fullElements.push({key: "new"});
  };

  private processLanguage(language) {
    let rtnLang = language;

    for(let i = 0; i < language.elements.length; i++) {
      var add = true;

      for (let j = 0; j < this.fullElements.length; j++) {
        if(this.fullElements[j].key == language.elements[i].key) {
          add = false;
        }
      }

      if(add) {
        this.fullElements.push({ key: language.elements[i].key});
      }
    }

    rtnLang.elems = {};

    return rtnLang;
  };

  private prepareForSave(language) {
    console.log(language);
    var lang = {
      languageId: language.languageId,
      name: language.name,
      active: language.active,
      elements: []
    };

    Object.keys(language.elems).forEach(k => {
      for(let i = 0; i < this.fullElements.length; i++) {
        if(k == this.fullElements[i].key) {
          lang.elements.push({
            key: k,
            text: language.elems[k]
          });
        }
      }
    });

    return lang;
  };

  constructor(
    private apiService: ApiService,
    private userService: UserService
  ) {
    this.loadLanguages();
  }
}
