import { Injectable, Inject } from "@angular/core";
import { ApiService } from "./api.service";
import { Topic } from "../models/topic";

@Injectable({
  providedIn: "root"
})
export class TopicService {
  constructor(private apiService: ApiService) {

  }

  public Search(searchText: String): Promise<Array<Topic>> {
    return this.apiService.get("/V1/Topic/Search?searchText=" + searchText).then(u => {
      return u;
    });
  }

}
