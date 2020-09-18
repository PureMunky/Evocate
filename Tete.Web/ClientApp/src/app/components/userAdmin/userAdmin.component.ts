import { Component } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { UserService } from "../../services/user.service";
import { User } from "../../models/user";

@Component({
  selector: "user-admin",
  templateUrl: "./userAdmin.component.html"
})
export class UserAdminComponent {

  public working = {
    searchText: '',
    roleName: ''
  };

  public users: Array<User> = [];

  constructor(
    private apiService: ApiService,
    private userService: UserService
  ) {
  }

  public search() {
    return this.apiService.get('/V1/User/Search?searchText=' + this.working.searchText).then(r => {
      this.users = r;
    });
  }

  public grantRole() {
    var checked = this.users.filter(u => u.checked);
    checked.forEach(async u => {
      await this.apiService.post('/V1/User/GrantRole', { userId: u.userId, name: this.working.roleName });
      u.checked = false;
    });
  }

  public revokeRole() {
    var checked = this.users.filter(u => u.checked);
    checked.forEach(async u => {
      await this.apiService.post('/V1/User/RemoveRole', { userId: u.userId, name: this.working.roleName });
      u.checked = false;
    });
  }

  public deleteAccounts() {
    var checked = this.users.filter(u => u.checked);
    checked.forEach(async u => {
      await this.apiService.post('/Login/AdminDelete', { userId: u.userId, name: '' });
      u.checked = false;
    });
  }
}
