import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { VideosViewRoutingModule } from './videosView-routing.module';


import { NewComponent } from './new/new.component';
import { UpComponent } from './up/up.component';
import { VideosCardComponent } from '../common/videos-card/videos-card.component';

@NgModule({
  imports: [
    VideosViewRoutingModule, CommonModule,
    NzInputModule, NzButtonModule, NzIconModule,
    NzCardModule, NzGridModule, NzSpinModule,
    NzEmptyModule, NzFormModule, NzAvatarModule,
    NzMessageModule, FormsModule, ReactiveFormsModule
  ],
  declarations: [NewComponent, VideosCardComponent, UpComponent],
  exports: [NewComponent]
})
export class VideosViewModule { }
