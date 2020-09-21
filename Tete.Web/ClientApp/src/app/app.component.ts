import { Component } from "@angular/core";
import { ErrorService } from "./services/error.service";
import { User } from "./models/user";
import { InitService } from "./services/init.service";
import { SettingService } from "./services/settings.service";
import { UserService } from "./services/user.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent {
  public currentUser: User = new User(null);
  public title = "app";
  public bannerMessage: string = ''

  public error: Error = {
    message: '',
    display: false,
    timer: setTimeout(() => { }, 1)
  };

  constructor(private errorService: ErrorService,
    private initService: InitService,
    private userService: UserService,
    private settingService: SettingService
  ) {
    errorService.Register(message => this.errorHandler(message));
    initService.Register(() => {
      this.currentUser = userService.CurrentUser();
      this.bannerMessage = this.settingService.Setting('system.message');
    })
  }

  public errorHandler(message: string) {
    if (this.error.timer) {
      clearTimeout(this.error.timer);
    }
    this.error.message = message;
    this.error.display = true;
    this.error.timer = setTimeout(() => this.error.display = false, 4000);
  }
}

interface Error {
  message: string;
  display: boolean;
  timer: any
}