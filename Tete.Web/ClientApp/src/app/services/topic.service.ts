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

  public Save(topic: Topic): Promise<any> {
    return this.apiService.post("/V1/Topic/Post", topic).then(t => {
      return t[0];
    });
  }

  public RegisterLearner(UserId: string, TopicId: string): Promise<any> {
    return this.apiService.post("/V1/Topic/RegisterLearner?UserId=" + UserId + "&TopicId=" + TopicId, {});
  }

  public RegisterMentor(UserId: string, TopicId: string): Promise<any> {
    return this.apiService.post("/V1/Topic/RegisterMentor?UserId=" + UserId + "&TopicId=" + TopicId, {});
  }

  public ClaimNextMentorship(UserId: string, TopicId: string): Promise<any> {
    return this.apiService.post("/V1/Topic/ClaimNextMentorship?UserId=" + UserId + "&TopicId=" + TopicId, {}).then(m => {
      return m[0];
    });
  }

  public GetTopic(topicId: string) {
    return this.apiService.get("/V1/Topic/GetTopic?topicId=" + topicId).then(t => {
      return t[0];
    });
  }

  public GetUserTopics(userId: string) {
    return this.apiService.get("/V1/Topic/GetUserTopics?userId=" + userId);
  }

  public GetTopTopics() {
    return this.apiService.get("/V1/Topic/GetTopTopics");
  }

  public GetNewestTopics() {
    return this.apiService.get("/V1/Topic/GetNewestTopics");
  }

  public GetWaitingTopics() {
    return this.apiService.get("/V1/Topic/GetWaitingTopics");
  }

  public GetKeywordTopics(keyword: string) {
    return this.apiService.get("/V1/Topic/GetKeywordTopics?keyword=" + keyword);
  }
}
