import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";

@Component({
  selector: "logging-component",
  templateUrl: "./logging.component.html"
})
export class LoggingComponent {
  public Logs;
  constructor(private apiService: ApiService) {
    this.apiService
      .get("/v1/Logs/Get")
      .then(result => {
        this.Logs = result;
      });
  }
}
