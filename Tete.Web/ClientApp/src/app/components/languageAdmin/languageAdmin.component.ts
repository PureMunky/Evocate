import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";

@Component({
  selector: "language-admin",
  templateUrl: "./languageAdmin.component.html"
})
export class LanguageAdminComponent {
  public Languages = [{ name: "none" }];
  public newLanguageInput = "";
  public currentLanguage;

  public addLanguage = function(newLanguage: String) {
    return this.apiService
      .get({
        url: "/v1/Languages",
        method: "Post",
        body: newLanguage
      })
      .then(() => this.loadLanguages());
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
        if (this.Languages.length > 0) {
          this.currentLanguage = this.Languages[0];
        }
      });
  };

  public addElement() {
    if (!this.currentLanguage.elements) {
      this.currentLanguage.elements = [];
    }
    this.currentLanguage.elements.push({ name: "", value: "" });
    console.log(this.currentLanguage);
    console.log(this.Languages);
  }

  constructor(private apiService: ApiService) {
    this.loadLanguages();
  }
}
