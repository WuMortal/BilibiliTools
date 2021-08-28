import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { fromEvent } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { DataApi, Episode } from '../../../apis/dataApi';

@Component({
  selector: 'videosView-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.css']
})
export class NewComponent implements OnInit {
  totalCount: number = 0;
  upId: string = '';
  upName: string = '';
  episodes: Episode[] = [];
  queryData!: FormGroup;
  pageIndex: number = 1;
  isSpinning: boolean = false;

  constructor(
    private api: DataApi,
    private fb: FormBuilder,
    private message: NzMessageService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit() {
    this.route.params.subscribe(
      params => {
        this.upId = params.upId;
        this.upName = params.upName;
      });

    fromEvent(document.getElementsByClassName('video-content'), 'scroll')
      .subscribe(event => {
        if (this.isBottom(event)) {
          this.pageIndex++;
          this.search();
        }
      });

    this.queryData = this.fb.group({
      queryText: null
    });
    this.search();
  }

  public onSubmit() {
    this.pageIndex = 1;
    this.episodes = [];
    this.search();
  }

  private search() {
    this.isSpinning = true;
    let param = {
      queryText: this.queryData.controls.queryText.value,
      uploaderId: this.upId,
      pageIndex: this.pageIndex,
      pageSize: 50
    };
    this.api.queryEpisode(param)
      .subscribe(
        result => {
          this.isSpinning = false;
          this.totalCount = result.totalCount;
          result.item.forEach(e => {
            e.coverUrl = `${environment.serverAddress}\\Resource\\${e.coverImagePath}`
          });

          if (result.item.length <= 0) {
            this.message.info('没有更多了');
            return;
          }

          this.episodes.push(...result.item);
        },
        error => {
          this.message.create('error', '加载失败');
        }
      );
  }

  private isBottom(event: any): boolean {
    return event.target.clientHeight + event.target.scrollTop >= event.target.scrollHeight;
  }
}

