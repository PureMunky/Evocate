import { Component, Input } from '@angular/core';
@Component({
  selector: 'teteTile',
  templateUrl: './tile.component.html'
})
export class TileComponent {
  @Input('title') title: string;
  @Input('subTitle') subTitle: string;
  @Input('body') body: string;
  @Input('url') url: string;
}