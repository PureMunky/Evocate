import { Component } from '@angular/core';
import { InitService } from '../services/init.service';
import { User } from '../models/user';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public currentUser: User = new User();
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  private profileLinkPrefix: string = '/profile/';
  public profileLink: string = '';

  constructor(
    private initService: InitService,
    private userServeice: UserService
  ) {
    initService.Register(() => {
      this.currentUser = userServeice.CurrentUser();
      this.profileLink = this.profileLinkPrefix + this.currentUser.userName;
    });
  }
}
