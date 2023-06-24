import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DriversManagementRoutingModule } from './drivers-management-routing.module';
import { ListComponent } from './Components/list/list.component';
import { AddEditComponent } from './Components/add-edit/add-edit.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    ListComponent,
    AddEditComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    DriversManagementRoutingModule,
    FormsModule
  ]
})
export class DriversManagementModule { }
