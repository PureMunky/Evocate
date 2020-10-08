import { Injectable, Inject } from "@angular/core";
import { ApiService } from "./api.service";
import { UserService } from "./user.service";

@Injectable({
  providedIn: "root"
})
export class LanguageService {
  constructor(
    private apiService: ApiService,
    private userService: UserService
  ) { }

  private languages;

  public Load() {
    return this.apiService.get("V1/Languages/Get")
      .then(result => {
        this.languages = result;
      });
  }

  public Element(key) {
    this.userService.CurrentUser().languages[0].elements[key];
  }

  public Languages() {
    return this.languages;
  }
}
