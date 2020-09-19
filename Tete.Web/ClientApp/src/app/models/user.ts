import { Profile } from "./profile";

export class User {
  public userId: string;
  public userName: string;
  public displayName: string;
  public profile: Profile;
  public languages: Array<any>;
  public roles: Array<string>;
  public checked: boolean;
  public block: {
    userId: string,
    endDate: Date,
    publicComments: string
  };

  public canAction(): boolean {
    var action = true;

    if (this.block !== null) {
      action = false;
    }

    if (this.roles.some(r => r == "Guest")) {
      action = false;
    }

    return action;
  }

  constructor() {
    this.userId = '';
    this.userName = '';
    this.displayName = '';
    this.profile = new Profile();
    this.languages = [];
    this.roles = [];
    this.checked = false;
    this.block = null;
  }
}