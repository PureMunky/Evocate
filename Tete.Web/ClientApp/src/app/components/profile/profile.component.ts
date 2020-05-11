import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { UserService } from "../../services/user.service";

@Component({
    selector: "profile",
    templateUrl: "./profile.component.html"
})
export class ProfileComponent {

    constructor(
        private apiService: ApiService,
        private userService: UserService
    ) {
        
    }
}