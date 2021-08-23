import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'video-card',
  templateUrl: './videos-card.component.html',
  styleUrls: ['./videos-card.component.css']
})
export class VideosCardComponent implements OnInit {

  videosCardClass = {
    'width': '160px',
    'height': '38px',
    'overflow': 'hidden',
    'line-height': '20px',
    'margin-top': '6px',
    'padding': '0px',
    'font-size': '12px'
  }

  @Input() video!: any;
  constructor() { }

  ngOnInit() {
  }

}