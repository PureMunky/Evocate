import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { LoadingService } from "./loading.service";
import { ErrorService } from "./error.service";

@Injectable({
  providedIn: "root"
})
export class ApiService {
  private user;
  private cache = {};
  private defaultCacheLimitMS: number = 0;

  constructor(private http: HttpClient,
    private loadingService: LoadingService,
    private errorService: ErrorService) {
  }

  get(url: string, timeout: number = this.defaultCacheLimitMS): Promise<any[]> {
    this.loadingService.Loading();
    var response: Promise<any[]>;
    var time = new Date();
    if (this.cache[url] && ((time.getTime() - this.cache[url].timestamp) <= timeout)) {
      // Load from local cache.
      this.loadingService.FinishedLoading();
      response = Promise.resolve(this.cache[url].data);
    } else {
      // Perform full round trip.
      response = this.http
        .get<Response>(url)
        .toPromise()
        .then(result => {
          this.loadingService.FinishedLoading();
          this.cache[url] = new Cache(url, result.data);
          return result.data;
        })
        .catch(err => this.handleError(err));
    }

    return response;
  }

  authTest() {
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

  post(url: string, body: object, flush: Array<string> = []): Promise<any[]> {
    this.loadingService.Loading();

    flush.forEach(flushUrl => {
      delete this.cache[flushUrl];
    });

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

class Cache {
  public url: string;
  public data: any[];
  public timestamp: Date;

  constructor(url: string, data: any[]) {
    this.url = url;
    this.data = data;
    this.timestamp = new Date();
  }
}