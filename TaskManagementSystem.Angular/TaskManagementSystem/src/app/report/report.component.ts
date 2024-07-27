import { Component, OnInit } from '@angular/core';
import { TaskReportRequest } from '../shared/models/task/taskReportRequestDto';
import { ConfirmationService } from 'primeng/api';
import { ProjectDto } from '../shared/models/project/ProjectDto';
import { UserDto } from '../shared/models/user/UserDto';
import { ApiHttpService } from '../shared/services/api-http.service';
import { commonService } from '../shared/services/common.service';
import { ToasterService } from '../shared/services/toaster.service';
import { TaskDto } from '../shared/models/task/TaskDto';
import { Table } from 'primeng/table';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {

  request: TaskReportRequest = <TaskReportRequest>{};
  taskList: TaskDto[] = [];
  projects: ProjectDto[] = [];
  users: UserDto[] = [];
  priorities: any[] = [];
  types: any[] = [];
  taskStatus: any[] = [];

  constructor(
    private _apiService: ApiHttpService,
    private _confirmationService: ConfirmationService,
    private _toasterService: ToasterService,
    private _commonService: commonService
  ) {
    this.priorities = this._commonService.priorities;
    this.types = this._commonService.types;
    this.taskStatus = this._commonService.status;
    this.getAllProject();
    this.getAllUsers();
  }

  ngOnInit(): void {
  }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }

  getAllUsers() {
    this._apiService.get(`User/Getall`)
      .subscribe((response: any) => {
        this.users = response.data;
      });
  }

  getAllProject() {
    this._apiService.get(`Project/Getall`)
      .subscribe((response: any) => {
        this.projects = response.data
      });
  }

  statusName(id: number) {
    return this.taskStatus.find(x => x.id == id).name;
  }

  priorityName(id: number) {
    return this.priorities.find(x => x.id == id).name;
  }

  typeName(id: number) {
    return this.types.find(x => x.id == id).name;
  }

  onSubmit(form: NgForm) {
    this._apiService.post(`Task/GetTaskReport`, this.request)
      .subscribe((response: any) => {
        this.taskList = response.data;
      });
  }

}
