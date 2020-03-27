import { Injectable } from "@angular/core";
import { UserService } from "./user.service";

@Injectable({
  providedIn: "root"
})
export class InitService {
  private functions = [];

  constructor(private userService: UserService) {
    let inits = [
      this.userService.Load()
    ];

    Promise.all(inits).then(() => {
      for (let i = 0; i < this.functions.length; i++) {
        this.functions[i]();
      }
    });
  }

  public Register(func: Function) {
    this.functions.push(func);
  }
}