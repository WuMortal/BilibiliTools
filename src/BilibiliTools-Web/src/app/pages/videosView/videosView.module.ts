import { NgModule } from '@angular/core';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { VideosViewRoutingModule } from './videosView-routing.module';


import { NewComponent } from './new/new.component';
import { VideosCardComponent } from '../common/videos-card/videos-card.component';

@NgModule({
  imports: [
    VideosViewRoutingModule,
    NzInputModule,
    NzButtonModule,
    NzIconModule,
    NzCardModule,
    NzGridModule
  ],
  declarations: [NewComponent, VideosCardComponent],
  exports: [NewComponent]
})
export class VideosViewModule { }
