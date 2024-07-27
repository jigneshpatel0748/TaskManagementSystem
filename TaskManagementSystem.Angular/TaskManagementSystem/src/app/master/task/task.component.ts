import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { Table } from 'primeng/table';
import { ProjectDto } from 'src/app/shared/models/project/ProjectDto';
import { TaskDto } from 'src/app/shared/models/task/TaskDto';
import { TaskRequestDto } from 'src/app/shared/models/task/TaskRequestDto';
import { UserDto } from 'src/app/shared/models/user/UserDto';
import { ApiHttpService } from 'src/app/shared/services/api-http.service';
import { commonService } from 'src/app/shared/services/common.service';
import { ToasterService } from 'src/app/shared/services/toaster.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss']
})
export class TaskComponent implements OnInit {

  taskList: TaskDto[] = [];
  taskEdit: TaskRequestDto = <TaskRequestDto>{};
  showDialog: boolean = false;
  projects: ProjectDto[] = [];
  users: UserDto[] = [];
  priorities: any[] = [];
  types: any[] = [];
  status: any[] = [];

  constructor(
    private _apiService: ApiHttpService,
    private _confirmationService: ConfirmationService,
    private _toasterService: ToasterService,
    private _commonService: commonService
  ) { }

  ngOnInit(): void {
    this.priorities = this._commonService.priorities;
    this.types = this._commonService.types;
    this.status = this._commonService.status;
    this.getAllTasks();
    this.getAllProject();
    this.getAllUsers();
  }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }

  getAllTasks() {
    this._apiService.get(`Task/Getall`)
      .subscribe((response: any) => {
        this.taskList = response.data;
      });
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

  editTask(task: any): void {
    this.taskEdit = { ...JSON.parse(JSON.stringify(task)) };
    this.taskEdit.assignDate = this.taskEdit.assignDate ? new Date(this.taskEdit.assignDate) : null;
    this.taskEdit.dueDate = this.taskEdit.dueDate ? new Date(this.taskEdit.dueDate) : null;
    this.showDialog = true;
  }

  statusName(id: number) {
    return this.status.find(x => x.id == id).name;
  }

  priorityName(id: number) {
    return this.priorities.find(x => x.id == id).name;
  }

  typeName(id: number) {
    return this.types.find(x => x.id == id).name;
  }

  deleteTask(task: TaskDto): void {
    if (task.id != null && task.id != 0) {
      this._apiService.delete(`Task/Delete/${task.id}`)
        .subscribe((response: any) => {
          if (response.isSuccess) {
            this._toasterService.toastSuccess(response.message);
            this.getAllTasks();
          } else {
            this._toasterService.toastError(response.message);
          }
        });
    }
  }

  addTask(form: NgForm): void {
    form.reset();
    this.taskEdit = <TaskRequestDto>{};
    this.showDialog = true;
  }

  onSubmit(form: NgForm) {

    if (this.taskEdit.id != null && this.taskEdit.id != 0) {
      this._apiService.put(`Task/Update/${this.taskEdit.id}`, this.taskEdit).subscribe((response: any) => {
        if (response.isSuccess) {
          this._toasterService.toastSuccess(response.message);
          this.getAllTasks();
          this.hideDialog(form);
        } else {
          this._toasterService.toastError(response.message);
        }
      });
    } else {
      this._apiService.post(`Task/Create`, this.taskEdit).subscribe((response: any) => {
        if (response.isSuccess) {
          this._toasterService.toastSuccess(response.message);
          this.getAllTasks();
          this.hideDialog(form);
        } else {
          this._toasterService.toastError(response.message);
        }
      });
    }
  }

  hideDialog(form: NgForm) {
    form.reset();
    this.taskEdit = <TaskRequestDto>{};
    this.showDialog = false;
  }

  deleteConfirm(event: Event, task: TaskDto) {
    this._confirmationService.confirm({
      target: (<Element>event.target),
      message: 'Are you sure that you want to proceed?',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteTask(task);
      },
      reject: () => {
        //reject action
      }
    });
  }

  onEventUpload(event: any) {
    this.taskEdit.attachment = event;
  }

}
