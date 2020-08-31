import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { UserService } from "../../services/user.service";
import { Link } from "../../models/link";

@Component({
  selector: "link-admin",
  templateUrl: "./linkAdmin.component.html"
})
export class LinkAdminComponent {
  public Links: Array<Link> = [];
  public AllLinks: Array<Link> = [];

  constructor(
    private apiService: ApiService,
    private userService: UserService
  ) {
    this.loadLinks();
  }

  public loadLinks() {
    return this.apiService.get("V1/Link/Get")
      .then(data => {
        this.AllLinks = data;
        this.filterAll();
      });
  };

  public saveLink(link: Link) {
    return this.apiService.post("V1/Link/Post", link);
  }

  public filterAll() {
    this.Links = this.AllLinks;
  }

  public filterUnreviewed() {
    this.Links = this.AllLinks.filter(l => !l.reviewed);
  }

  public filterActive() {
    this.Links = this.AllLinks.filter(l => l.active);
  }

  /*
  public addLanguage = function () {
    this.Languages.push({
      LanguageId: "",
      name: "new",
      active: true,
      elements: [],
      elems: {}
    });
  };

  public saveLanguage(language) {
    var lang = this.prepareForSave(language);
    var rtnPromise;

    if (lang.languageId == null) {
      rtnPromise = this.apiService.post("V1/Languages/Post", lang);
    } else {
      rtnPromise = this.apiService.put("V1/Languages/Update", lang);
    }

    return rtnPromise.then(() => { this.loadLanguages() });
  };



  public addElement() {
    this.fullElements.push({ key: "new" });
  };

  private processLanguage(language) {
    let rtnLang = language;
    rtnLang.elems = {};

    for (let i = 0; i < language.elements.length; i++) {
      var add = true;
      rtnLang.elems[language.elements[i].key] = language.elements[i].text;

      for (let j = 0; j < this.fullElements.length; j++) {
        if (this.fullElements[j].key == language.elements[i].key) {
          add = false;
        }
      }

      if (add) {
        this.fullElements.push({ key: language.elements[i].key });
      }
    }

    return rtnLang;
  };

  private prepareForSave(language) {
    var lang = {
      languageId: language.languageId,
      name: language.name,
      active: language.active,
      elements: []
    };

    Object.keys(language.elems).forEach(k => {
      for (let i = 0; i < this.fullElements.length; i++) {
        if (k == this.fullElements[i].key) {
          lang.elements.push({
            key: k,
            text: language.elems[k]
          });
        }
      }
    });

    return lang;
  };
 */
}
