import { Component } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { SystemApi } from './apis/SystemApi';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isCollapsed = false;
  isSpinning = false;

  constructor(
    private api: SystemApi,
    private message: NzMessageService
  ) { }

  public executeScan() {
    this.isSpinning = true;
    this.api.runAnalyze()
      .subscribe(
        result => {
          this.isSpinning = false;

          this.message.success(`扫描完成，共扫描到 ${result} 个视频`, {
            nzDuration: 5000,
          }).onClose!.subscribe(() => {

          });
        },
        error => {
          this.message.create('error', '加载失败');
        }
      );
  }
}
