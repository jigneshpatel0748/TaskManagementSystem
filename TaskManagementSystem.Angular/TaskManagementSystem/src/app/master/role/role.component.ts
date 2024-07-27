import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { Table } from 'primeng/table';
import { RoleDto } from 'src/app/shared/models/role/RoleDto';
import { ApiHttpService } from 'src/app/shared/services/api-http.service';
import { ToasterService } from 'src/app/shared/services/toaster.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})
export class RoleComponent implements OnInit {

  roleList: RoleDto[] = [];
  roleEdit: RoleDto = <RoleDto>{};
  showDialog: boolean = false;
  roles: RoleDto[] = [];

  constructor(
    private _apiService: ApiHttpService,
    private _confirmationService: ConfirmationService,
    private _toasterService: ToasterService
  ) { }

  ngOnInit(): void {
    this.getAllRoles();
  }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }

  getAllRoles() {
    this._apiService.get(`Role/Getall`)
      .subscribe((response: any) => {
        this.roleList = response.data
      });
  }

  editRole(role: RoleDto): void {
    this.roleEdit = JSON.parse(JSON.stringify(role));
    this.showDialog = true;
  }

  deleteRole(role: RoleDto): void {
    if (role.id != null && role.id != 0) {
      this._apiService.delete(`Role/Delete/${role.id}`)
        .subscribe((response: any) => {
          if (response.isSuccess) {
            this._toasterService.toastSuccess(response.message);
            this.getAllRoles();
          } else {
            this._toasterService.toastError(response.message);
          }
        });
    }
  }

  addRole(form: NgForm): void {
    form.reset();
    this.roleEdit = <RoleDto>{};
    this.showDialog = true;
  }

  onSubmit(form: NgForm) {
    let payload = {
      name: this.roleEdit.name,
      isActive: this.roleEdit.isActive
    };

    if (this.roleEdit.id != null && this.roleEdit.id != 0) {
      this._apiService.put(`Role/Update/${this.roleEdit.id}`, payload).subscribe((response: any) => {
        if (response.isSuccess) {
          this._toasterService.toastSuccess(response.message);
          this.getAllRoles();
          this.hideDialog(form);
        } else {
          this._toasterService.toastError(response.message);
        }
      });
    } else {
      this._apiService.post(`Role/Create`, payload).subscribe((response: any) => {
        if (response.isSuccess) {
          this._toasterService.toastSuccess(response.message);
          this.getAllRoles();
          this.hideDialog(form);
        } else {
          this._toasterService.toastError(response.message);
        }
      });
    }
  }

  hideDialog(form: NgForm) {
    form.reset();
    this.roleEdit = <RoleDto>{};
    this.showDialog = false;
  }

  deleteConfirm(event: Event, role: RoleDto) {
    this._confirmationService.confirm({
      target: (<Element>event.target),
      message: 'Are you sure that you want to proceed?',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteRole(role);
      },
      reject: () => {
        //reject action
      }
    });
  }

}
