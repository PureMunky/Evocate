import { Injectable, Inject } from "@angular/core";
import { ApiService } from "./api.service";

@Injectable({
  providedIn: "root"
})
export class UserService {
  constructor(private apiService: ApiService) {

  }
  private currentUser;

  public Load() {
    return this.apiService.authTest().then(u => {
      this.currentUser = u;
    });
  }

  public CurrentUser() {
    return this.currentUser;
  }
}
