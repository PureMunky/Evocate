import { Injectable, Inject } from "@angular/core";
import { ApiService } from "./api.service";
import { User } from "../models/user";

@Injectable({
  providedIn: "root"
})
export class SettingService {
  constructor(private apiService: ApiService) {

  }
  private settings = [];

  public Load() {
    return this.apiService.get("V1/Settings/Get")
      .then(data => {
        this.settings = data;
      });
  }

  public Setting(key: string): string {
    let filteredSettings = this.settings.filter(s => s.Key == key);
    let rtnSetting = { Key: '', Value: '' };

    if (filteredSettings.length > 0) {
      rtnSetting = filteredSettings[0];
    }

    return rtnSetting.Value;
  }

}