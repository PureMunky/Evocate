import { Component, Input } from '@angular/core';
import { Topic } from 'src/app/models/topic';
import { Mentorship } from 'src/app/models/mentorship';
@Component({
  selector: 'teteTile',
  templateUrl: './tile.component.html'
})
export class TileComponent {
  @Input('topic') topic: Topic;
  @Input('mentorship') mentorship: Mentorship;
}