import { Injectable } from "@angular/core";
import { UserService } from "./user.service";
import { LanguageService } from "./language.service";

@Injectable({
  providedIn: "root"
})
export class InitService {
  private functions = [];
  private loaded = false;

  constructor(private userService: UserService,
    private languageService: LanguageService) {
    this.Load();
  }

  public Load() {
    let inits = [
      this.userService.Load(),
      this.languageService.Load()
    ];

    Promise.all(inits).then(() => {
      this.loaded = true;
      for (let i = 0; i < this.functions.length; i++) {
        this.functions[i]();
      }
    });
  }

  public Register(func: Function) {
    if(this.loaded) {
      func();
    } else {
      this.functions.push(func);
    }
  }
}