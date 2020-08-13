import { Component, Input } from '@angular/core';
import { Topic } from '../../models/topic';

@Component({
  selector: 'teteTopicLink',
  template: `<a [routerLink]="['/topic/', topic.topicId]">{{topic.name}}</a>`
})
export class TopicLink {
  @Input('topic') topic: Topic;
}