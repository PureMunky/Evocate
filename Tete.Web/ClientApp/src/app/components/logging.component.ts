import { Component } from "@angular/core";
import { ApiService } from "../services/api.service";

@Component({
  selector: "logging-component",
  templateUrl: "./logging.component.html"
})
export class LoggingComponent {
  private Logs;
  constructor(private apiService: ApiService) {
    this.Logs = apiService.get({
      Url: "/v1/Logs",
      Method: "Get",
      Body: ""
    });
  }
}
