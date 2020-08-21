import { UserTopic } from './userTopic';
import { Keyword } from "./keyword";
import { Link } from "./link";

export class Topic {
  public topicId: string;
  public name: string;
  public description: string;
  public elligible: boolean;
  public mentorshipCount: number;
  public userTopic: UserTopic;
  public keywords: Array<Keyword>;
  public links: Array<Link>;

  constructor() {
    this.name = '';
    this.description = '';
    this.elligible = false;
    this.mentorshipCount = 0;
    this.userTopic = new UserTopic();
    this.keywords = [];
    this.links = [];
  }
}