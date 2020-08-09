import { UserTopic } from './userTopic';

export class Topic {
  public topicId: string;
  public name: string;
  public description: string;
  public elligible: boolean;
  public mentorshipCount: number;
  public userTopic: UserTopic;

  constructor() {
    this.name = '';
    this.description = '';
    this.elligible = false;
    this.mentorshipCount = 0;
    this.userTopic = new UserTopic();
  }
}