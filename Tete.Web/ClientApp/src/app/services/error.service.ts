import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class ErrorService {
  private functions = [];

  public Error(message: string) {
    for (let i = 0; i < this.functions.length; i++) {
      this.functions[i](message);
    }
  }

  public Register(func: Function) {
    this.functions.push(func);
  }
}