import { Profile } from "./profile";

export class User {
  public userId: string;
  public userName: String;
  public displayName: String;
  public profile: Profile;
  public languages: Array<any>;

  constructor() {
    this.userId = '';
    this.userName = '';
    this.displayName = '';
    this.profile = new Profile();
    this.languages = [];
  }
}