import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { DataApi, Uploader } from '../../../apis/dataApi';

@Component({
  selector: 'app-up',
  templateUrl: './up.component.html',
  styleUrls: ['./up.component.css']
})
export class UpComponent implements OnInit {

  ups: Uploader[] = [];

  constructor(
    private api: DataApi,
    private message: NzMessageService
  ) { }

  ngOnInit() {
    this.api.getAllUploader()
      .subscribe(
        result => {

          if (result.length <= 0) {
            this.message.info('没有更多了');
            return;
          }

          this.ups.push(...result);
        },
        error => {
          this.message.create('error', '加载失败');
        }
      );
  }

}