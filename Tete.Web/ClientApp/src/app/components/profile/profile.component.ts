import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { UserService } from "../../services/user.service";
import { InitService } from "../../services/init.service";
import { LanguageService } from "../../services/language.service";

@Component({
    selector: "profile",
    templateUrl: "./profile.component.html"
})
export class ProfileComponent {
    public user = {
        displayName: ''
    };
    public languages = [];

    constructor(
        private apiService: ApiService,
        private userService: UserService,
        private initService: InitService,
        private languageService: LanguageService
    ) {
        initService.Register(() => {
            this.user = userService.CurrentUser();
            this.languages = languageService.Languages();
        });
    }
}