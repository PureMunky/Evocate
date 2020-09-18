import { Profile } from "./profile";

export class User {
  public userId: string;
  public userName: string;
  public displayName: string;
  public profile: Profile;
  public languages: Array<any>;
  public roles: Array<string>;
  public checked: boolean;

  constructor() {
    this.userId = '';
    this.userName = '';
    this.displayName = '';
    this.profile = new Profile();
    this.languages = [];
    this.roles = [];
    this.checked = false;
  }
}