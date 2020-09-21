import { Injectable, Inject } from "@angular/core";
import { ApiService } from "./api.service";
import { User } from "../models/user";

@Injectable({
  providedIn: "root"
})
export class UserService {
  constructor(private apiService: ApiService) {

  }
  private currentUser: User;

  public Load() {
    return this.apiService.authTest().then(u => {
      this.currentUser = u as User;
    });
  }

  public Get(userName: String): Promise<User> {
    return this.apiService.get("/Login/GetUser?username=" + userName).then(u => {
      return u[0] as User;
    });
  }

  public CurrentUser(): User {
    return new User(this.currentUser);
  }

}