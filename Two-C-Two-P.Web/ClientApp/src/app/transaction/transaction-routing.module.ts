import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { ViewComponent } from "./view/view.component";
import { UploadComponent } from "./upload/upload.component";
import { TransactionComponent } from "./transaction.component";

const routes: Routes = [
  {
    path: '',
    component: TransactionComponent,
    children: [
      {
        path: '',
        component: ViewComponent
      },
      {
        path: 'upload',
        component: UploadComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TransactionRoutingModule { }
