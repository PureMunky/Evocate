import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class ApiService {
  private http: HttpClient;
  private baseUrl: string;
  private user;

  constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  get(request: Request) {
    return this.http
      .post<Response>(this.baseUrl + "api/Request", request)
      .toPromise()
      .then(result => {
          return result.data;
      })
      .catch(this.handleError);
  }

  authTest() {
    return this.http
      .get(this.baseUrl + "Login/CurrentUser")
      .toPromise()
      .then(user => {
        console.log(user);
        this.user = user;
        return user;
      })
      .catch(this.handleError);
  }

  post(url: string, body: object) {
    return this.http
      .post(this.baseUrl + url, body)
      .toPromise()
      .catch(this.handleError);
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status == 401) {
      // UnAuthorized
      window.location.href = "/Login";
    }
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
