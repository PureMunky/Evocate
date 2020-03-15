import { Component } from "@angular/core";
import { ApiService } from "../services/api.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html"
})
export class HomeComponent {
  constructor(private apiService: ApiService) {
    apiService.authTest();
  }
}
