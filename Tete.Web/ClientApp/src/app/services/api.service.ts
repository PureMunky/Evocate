import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class ApiService {
  private http: HttpClient;
  private user;

  constructor(http: HttpClient) {
    this.http = http;
  }

  get(url):Promise<Object> {
    return this.http
      .get<Response>(url)
      .toPromise<Response>()
      .then(result => {
        return result.data;
      })
      .catch(this.handleError);
  }

  authTest() {
    return this.http
      .get("/Login/CurrentUser")
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
      .post(url, body)
      .toPromise()
      .catch(this.handleError);
  }

  put(url: string, body: object) {
    return this.http
      .put(url, body)
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
