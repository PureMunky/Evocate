import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { LoadingService } from "./loading.service";
import { ErrorService } from "./error.service";

@Injectable({
  providedIn: "root"
})
export class ApiService {
  private user;

  constructor(private http: HttpClient,
    private loadingService: LoadingService,
    private errorService: ErrorService) {
  }

  get(url): Promise<any[]> {
    this.loadingService.Loading();
    return this.http
      .get<Response>(url)
      .toPromise()
      .then(result => {
        this.loadingService.FinishedLoading();
        return result.data;
      })
      .catch(err => this.handleError(err));
  }

  authTest() {
    // TODO: Add authtest to each call but also not require it to be a full round trip on most cases.
    // TODO: Determine the "logged out" functionality for if someone comes to the site before logging in.
    this.loadingService.Loading();
    return this.http
      .get("/Login/CurrentUser")
      .toPromise()
      .then(user => {
        this.user = user;
        this.loadingService.FinishedLoading();
        return user;
      })
      .catch(err => this.handleError(err));
  }

  post(url: string, body: object): Promise<any[]> {
    this.loadingService.Loading();
    return this.http
      .post<Response>(url, body)
      .toPromise()
      .then(res => {
        this.loadingService.FinishedLoading();
        return res.data;
      })
      .catch(err => this.handleError(err));
  }

  put(url: string, body: object) {
    return this.http
      .put(url, body)
      .toPromise()
      .catch(err => this.handleError(err));
  }

  private handleError(error: HttpErrorResponse) {
    this.errorService.Error('Loading Error');
    this.loadingService.FinishedLoading();
    if (error.status == 401) {
      // UnAuthorized
      window.location.href = "/Login";
    }
    return [{}];
  }
}

interface Response {
  request: Request;
  data: object[];
  error: boolean;
  message: string;
  status: number;
}

interface Request {
  url: string;
  method: string;
  body: string;
}
