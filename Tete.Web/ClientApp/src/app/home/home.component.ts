import { Component } from "@angular/core";
import { InitService } from "../services/init.service";
import { UserService } from "../services/user.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html"
})
export class HomeComponent {
  public userName = 'world';

  constructor(private userService: UserService, private initService: InitService) {
    initService.Register(() => {
      this.userName = userService.CurrentUser().displayName;
    });
  }
}
