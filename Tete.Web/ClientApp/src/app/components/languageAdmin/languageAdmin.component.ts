import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";

@Component({
  selector: "language-admin",
  templateUrl: "./languageAdmin.component.html"
})
export class LanguageAdminComponent {
  public Languages;

  constructor(private apiService: ApiService) {
    apiService
      .get({
        url: "/v1/Languages",
        method: "Get",
        body: ""
      })
      .then(result => {
        this.Languages = result;
      });
  }
}
