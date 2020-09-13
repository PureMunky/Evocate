import { Injectable, Inject } from "@angular/core";
import { ApiService } from "./api.service";
import { User } from "../models/user";

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

  public Get(userName: String): Promise<User> {
    return this.apiService.get("/Login/GetUser?username=" + userName).then(u => {
      return u[0];
    });
  }

  public CurrentUser() {
    return this.currentUser;
  }

}
