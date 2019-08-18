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
  get(request: Request) {
    return this.http
      .post<Response>(this.baseUrl + "api/Request", request)
      .toPromise()
      .then(
        result => {
          return result.data;
        },
        error => console.error(error)
      );
  }
}

interface Response {
  request: Request;
  data: string;
  error: boolean;
  message: string;
  status: number;
}

interface Request {
  url: string;
  method: string;
  body: string;
}
