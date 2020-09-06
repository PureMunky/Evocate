import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class LoadingService {
  private functions = [];

  public Loading() {
    for (let i = 0; i < this.functions.length; i++) {
      this.functions[i](true);
    }
  }

  public FinishedLoading() {
    for (let i = 0; i < this.functions.length; i++) {
      this.functions[i](false);
    }
  }

  public Register(func: Function) {
    this.functions.push(func);
  }
}