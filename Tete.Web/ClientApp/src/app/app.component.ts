import { Component } from "@angular/core";
import { ErrorService } from "./services/error.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent {
  public title = "app";

  public error: Error = {
    message: '',
    display: false,
    timer: setTimeout(() => { }, 1)
  };

  constructor(private errorService: ErrorService) {
    errorService.Register(message => this.errorHandler(message));
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