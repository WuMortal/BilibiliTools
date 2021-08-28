import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NewComponent } from './new/new.component';
import { UpComponent } from './up/up.component';

const routes: Routes = [
  { path: 'new', component: NewComponent },
  { path: 'new/:upId/:upName', component: NewComponent },
  { path: 'up', component: UpComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VideosViewRoutingModule { }
