import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MasterRoutingModule } from './master-routing.module';
import { RoleComponent } from './role/role.component';
import { UserComponent } from './user/user.component';
import { TaskComponent } from './task/task.component';
import { ProjectComponent } from './project/project.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    RoleComponent,
    UserComponent,
    TaskComponent,
    ProjectComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    MasterRoutingModule
  ]
})
export class MasterModule { }
