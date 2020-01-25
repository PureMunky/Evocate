import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";

@Component({
  selector: "language-admin",
  templateUrl: "./languageAdmin.component.html"
})
export class LanguageAdminComponent {
  public Languages;
  public newLanguageInput = "";

  public addLanguage = function(newLanguage: String) {
    return this.apiService
      .get({
        url: "/v1/Languages",
        method: "Post",
        body: newLanguage
      })
      .then(this.loadLanguages);
  };

  public loadLanguages = function() {
    return this.apiService
      .get({
        url: "/v1/Languages",
        method: "Get",
        body: ""
      })
      .then(result => {
        this.Languages = result;
      });
  };

  constructor(private apiService: ApiService) {
    this.loadLanguages();
  }
}
