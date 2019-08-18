import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Subscription } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class ApiService {
  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }
  get(request: Request): Subscription {
    return this.http
      .post<Response>(this.baseUrl + "api/Request", request)
      .subscribe(
        result => {
          return result.Data;
        },
        error => console.error(error)
      );
  }
}

interface Response {
  Request: Request;
  Data: string;
  Error: boolean;
  Message: string;
  Status: number;
}

interface Request {
  Url: string;
  Method: string;
  Body: string;
}
