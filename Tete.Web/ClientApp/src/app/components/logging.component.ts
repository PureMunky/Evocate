import { Component } from "@angular/core";
import { ApiService } from "../services/api.service";

@Component({
  selector: "logging-component",
  templateUrl: "./logging.component.html"
})
export class LoggingComponent {
  public Logs;
  constructor(private apiService: ApiService) {
    apiService
      .get({
        url: "/v1/Logs",
        method: "Get",
        body: ""
      })
      .then(result => {
        this.Logs = result;
      });
  }
}
