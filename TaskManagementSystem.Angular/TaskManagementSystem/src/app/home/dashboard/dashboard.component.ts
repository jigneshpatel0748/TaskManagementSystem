import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { TaskNoteRequestDto } from 'src/app/shared/models/task/NoteRequestDto';
import { TaskNoteDto } from 'src/app/shared/models/task/NotesDto';
import { TaskDto } from 'src/app/shared/models/task/TaskDto';
import { ApiHttpService } from 'src/app/shared/services/api-http.service';
import { commonService } from 'src/app/shared/services/common.service';
import { ToasterService } from 'src/app/shared/services/toaster.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  tasks: TaskDto[] = [];
  priorities: any[] = [];
  types: any[] = [];
  status: any[] = [];
  userTaskStatus: any[] = [];
  showDialog: boolean = false;
  showActionDialog: boolean = false;
  taskStatus: any = {
    status: 0,
    id: 0
  };
  editNote: TaskNoteRequestDto = <TaskNoteRequestDto>{};

  constructor(
    private _apiService: ApiHttpService,
    private _commonService: commonService,
    private _toasterService: ToasterService
  ) { }

  ngOnInit(): void {
    this.priorities = this._commonService.priorities;
    this.types = this._commonService.types;
    this.status = this._commonService.status;
    this.getTasks();
  }

  getTasks() {
    this._apiService.get(`Task/GetUserTask`)
      .subscribe((response: any) => {
        this.tasks = response.data;
      });
  }

  statusName(id: string) {
    return this.status.find(x => x.id == id).name;
  }

  getLabels(id: string) {
    let status = this.statusName(id);
    if (status == "Running") {
      return "Stop"
    } else {
      return "Start"
    }
  }

  showStartButton(id: string) {
    let status = this.statusName(id);
    if (status == "Complete" || status == "Cancel") {
      return false;
    } else {
      return true;
    }
  }

  priorityName(id: number) {
    return this.priorities.find(x => x.id == id).name;
  }

  typeName(id: number) {
    return this.types.find(x => x.id == id).name;
  }

  taskOpen(task: TaskDto) {
    this.editNote = <TaskNoteRequestDto>{};
    this.editNote.taskId = task.id;
    this.showDialog = true;
  }

  taskAction(task: TaskDto) {
    this.taskStatus.id = task.id;
    let currentLable = this.getLabels(task.status);
    if (currentLable == "Stop") {
      this.userTaskStatus = this.status.filter(x => x.name == "Pending" || x.name == "Complete" || x.name == "Cancel");
      this.showActionDialog = true;
    } else {
      this.taskStatus.status = 2;
      this.updateTaskStatus();
    }
  }

  hideActionDialog(form: NgForm) {
    form.reset();
    this.taskStatus = {
      status: 0,
      id: 0
    };
    this.showActionDialog = false;
  }

  hideDialog(form: NgForm) {
    form.reset();
    this.editNote = <TaskNoteRequestDto>{};
    this.showDialog = false;
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      this._apiService.post(`TaskNote/Create`, this.editNote).subscribe((response: any) => {
        if (response.isSuccess) {
          this._toasterService.toastSuccess(response.message);
          this.getTasks();
          this.hideDialog(form);
        } else {
          this._toasterService.toastError(response.message);
        }
      });
    }
  }

  onActionSubmit(form: NgForm) {
    if (form.valid) {
      this.updateTaskStatus();
      this.hideActionDialog(form);
    }
  }

  updateTaskStatus() {
    this._apiService.post(`Task/TaskAction/${this.taskStatus.id}/Status/${this.taskStatus.status}`, this.editNote).subscribe((response: any) => {
      if (response.isSuccess) {
        this.getTasks();
      } else {
        this._toasterService.toastError(response.message);
      }
    });
  }

  onEventUpload(event: any) {
    this.editNote.attachment = event;
  }

  download(note: TaskNoteDto) {
    if (note.attachment) {
      window.open(note.attachment, '_blank');
    }
  }

}
