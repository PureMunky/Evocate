import { Component } from "@angular/core";
import { UserService } from "./services/user.service";
import { LanguageService } from "./services/language.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent {
  title = "app";
}
