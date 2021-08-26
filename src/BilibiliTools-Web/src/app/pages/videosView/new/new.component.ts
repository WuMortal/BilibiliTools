import { Component, OnInit } from '@angular/core';

interface Video {
  id: string;
  coverUrl: string;
  title: string;
}

@Component({
  selector: 'videosView-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.css']
})
export class NewComponent implements OnInit {
  videos: Video[] = [];

  constructor() { }

  ngOnInit() {
    for (let index = 1; index <= 100; index++) {
      let video: Video = {
        id: `${index}`,
        coverUrl: '/assets/images/759ab8ea5c8ee1da37b179e83f9faadb4f4985d4.jpg@380w_240h_100Q_1c.webp',
        title: `${index} -【半小时哲学】为什么世界上会有一群人而不是只有我自己，为什么唯我论必然是错的（哪怕是最高级版本）——先验主观主义 vs 辩证唯物主义`
      };
      this.videos.push(video);
    }
  }

  getAllVideos() {

    const sqlite3 = require('sqlite3').verbose();
    let database = new sqlite3.Database('E:/Projects/BilibiliTools/src/BilibiliTools/bin/Debug/netcoreapp3.1/info.db3');
  }
}

