import { Profile } from "./profile";

export class User {
  public userId: string;
  public userName: string;
  public displayName: string;
  public profile: Profile;
  public languages: Array<any>;
  public roles: Array<string>;
  public checked: boolean;
  public loaded: boolean;
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
    
    if(!this.loaded) {
      action = false;
    }

    return action;
  }

  constructor(inner: User) {
    if (inner) {
      this.userId = inner.userId;
      this.userName = inner.userName;
      this.displayName = inner.displayName;
      this.profile = inner.profile;
      this.languages = inner.languages;
      this.roles = inner.roles;
      this.checked = false;
      this.block = inner.block;
      this.loaded = true;
    } else {
      this.userId = '';
      this.userName = '';
      this.displayName = '';
      this.profile = new Profile();
      this.languages = [];
      this.roles = [];
      this.checked = false;
      this.block = null;
      this.loaded = false;
    }
  }
}