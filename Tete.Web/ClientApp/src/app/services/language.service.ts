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
  ) {}

  public Element(key) {
    this.userService.CurrentUser().Languages[0].elements[key];
  }
}
