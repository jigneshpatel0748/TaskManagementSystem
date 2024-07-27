import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { Table } from 'primeng/table';
import { ProjectDto } from 'src/app/shared/models/project/ProjectDto';
import { ApiHttpService } from 'src/app/shared/services/api-http.service';
import { ToasterService } from 'src/app/shared/services/toaster.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

  projectList: ProjectDto[] = [];
  projectEdit: ProjectDto = <ProjectDto>{};
  showDialog: boolean = false;
  projects: ProjectDto[] = [];

  constructor(
    private _apiService: ApiHttpService,
    private _confirmationService: ConfirmationService,
    private _toasterService: ToasterService
  ) { }

  ngOnInit(): void {
    this.getAllProjects();
  }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }

  getAllProjects() {
    this._apiService.get(`Project/Getall`)
      .subscribe((response: any) => {
        this.projectList = response.data
      });
  }

  editProject(project: ProjectDto): void {
    this.projectEdit = JSON.parse(JSON.stringify(project));
    this.showDialog = true;
  }

  deleteProject(project: ProjectDto): void {
    if (project.id != null && project.id != 0) {
      this._apiService.delete(`Project/Delete/${project.id}`)
        .subscribe((response: any) => {
          if (response.isSuccess) {
            this._toasterService.toastSuccess(response.message);
            this.getAllProjects();
          } else {
            this._toasterService.toastError(response.message);
          }
        });
    }
  }

  addProject(form: NgForm): void {
    form.reset();
    this.projectEdit = <ProjectDto>{};
    this.showDialog = true;
  }

  onSubmit(form: NgForm) {
    let payload = {
      name: this.projectEdit.name,
      isActive: this.projectEdit.isActive
    };

    if (this.projectEdit.id != null && this.projectEdit.id != 0) {
      this._apiService.put(`Project/Update/${this.projectEdit.id}`, payload).subscribe((response: any) => {
        if (response.isSuccess) {
          this._toasterService.toastSuccess(response.message);
          this.getAllProjects();
          this.hideDialog(form);
        } else {
          this._toasterService.toastError(response.message);
        }
      });
    } else {
      this._apiService.post(`Project/Create`, payload).subscribe((response: any) => {
        if (response.isSuccess) {
          this._toasterService.toastSuccess(response.message);
          this.getAllProjects();
          this.hideDialog(form);
        } else {
          this._toasterService.toastError(response.message);
        }
      });
    }
  }

  hideDialog(form: NgForm) {
    form.reset();
    this.projectEdit = <ProjectDto>{};
    this.showDialog = false;
  }

  deleteConfirm(event: Event, project: ProjectDto) {
    this._confirmationService.confirm({
      target: (<Element>event.target),
      message: 'Are you sure that you want to proceed?',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteProject(project);
      },
      reject: () => {
        //reject action
      }
    });
  }

}
