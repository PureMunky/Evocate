import { Component } from '@angular/core';
import { InitService } from '../services/init.service';
import { User } from '../models/user';
import { UserService } from '../services/user.service';
import { LoadingService } from '../services/loading.service';
import { SettingService } from '../services/settings.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public currentUser: User = new User(null);
  public adminRole: boolean = false;
  public loading: boolean = true;
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  constructor(
    private initService: InitService,
    private userService: UserService,
    private loadingService: LoadingService,
    private settingService: SettingService
  ) {
    loadingService.Register(loading => this.loadingHandler(loading));
    initService.Register(() => {
      this.currentUser = userService.CurrentUser();
      this.adminRole = this.currentUser.roles.some(r => r == 'Admin');
    });
  }

  private loadingHandler(loading) {
    this.loading = loading;
  }
}
