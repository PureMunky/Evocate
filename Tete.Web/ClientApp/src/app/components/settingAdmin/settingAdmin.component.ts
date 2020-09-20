import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { UserService } from "../../services/user.service";

@Component({
  selector: "setting-admin",
  templateUrl: "./settingAdmin.component.html"
})
export class SettingAdminComponent {
  public settings = [];
  public working = {
    key: '',
    value: ''
  };

  constructor(
    private apiService: ApiService,
    private userService: UserService
  ) {
    this.loadSettings();
  }

  public loadSettings() {
    return this.apiService.get("V1/Settings/Get")
      .then(data => {
        this.settings = data;
      });
  };

  public save(setting) {
    return this.apiService.post("V1/Settings/Post", setting);
  }

  public async create() {
    await this.save(this.working);
    this.working.key = '';
    this.working.value = '';
  }

}
