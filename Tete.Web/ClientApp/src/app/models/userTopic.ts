export class UserTopic {
  public userTopicId: string;
  public status: number;
  public statusText: string;
  public topicId: string;
  public userId: string;
  public createdDate: Date;

  constructor() {
    this.status = 0;
    this.statusText = 'None';
    this.createdDate = new Date();
  }
}