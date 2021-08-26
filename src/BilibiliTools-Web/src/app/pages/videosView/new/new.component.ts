import { Component, OnInit } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { DataApi, Episode } from '../../../apis/dataApi';

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
  episodes: Episode[] = [];

  constructor(private api: DataApi) { }

  ngOnInit() {
    this.api.getEpisode()
      .subscribe(
        result => {

          result.forEach(e => {
            e.coverUrl = `${environment.serverAddress}\\Resource\\${e.coverImagePath}`
          });

          this.episodes = result;
        },
        error => {
        }
      );
  }
}

