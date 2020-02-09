import { Injectable, Inject } from "@angular/core";
import { ApiService } from "./api.service";

@Injectable({
  providedIn: "root"
})
export class UserService {
  constructor(private apiService: ApiService) {
    this.apiService.authTest().then(u => {
      this.currentUser = u;
      console.log("current user: ", this.currentUser);
    });
  }
  private currentUser;

  public CurrentUser() {
    return this.currentUser;
  }
}
