import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'videosView-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.css']
})
export class NewComponent implements OnInit {
  video = {
    coverUrl: '/assets/images/759ab8ea5c8ee1da37b179e83f9faadb4f4985d4.jpg@380w_240h_100Q_1c.webp',
    title: '【半小时哲学】为什么世界上会有一群人而不是只有我自己，为什么唯我论必然是错的（哪怕是最高级版本）——先验主观主义 vs 辩证唯物主义'
  }
  constructor() { }

  ngOnInit() {
  }

}