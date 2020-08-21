import { Component, Input } from '@angular/core';
import { User } from '../../models/user';

@Component({
  selector: 'teteProfileLink',
  template: `<a [routerLink]="['/profile/', user.userName]">{{user.displayName}}</a>`
})
export class ProfileLink {
  @Input('user') user: User
}