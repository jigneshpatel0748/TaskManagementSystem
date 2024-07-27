import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { Table } from 'primeng/table';
import { RoleDto } from 'src/app/shared/models/role/RoleDto';
import { UserDto } from 'src/app/shared/models/user/UserDto';
import { ApiHttpService } from 'src/app/shared/services/api-http.service';
import { ToasterService } from 'src/app/shared/services/toaster.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  userList: UserDto[] = [];
  userEdit: UserDto = <UserDto>{};
  showDialog: boolean = false;
  users: UserDto[] = [];
  roles: RoleDto[] = [];

  constructor(
    private _apiService: ApiHttpService,
    private _confirmationService: ConfirmationService,
    private _toasterService: ToasterService
  ) { }

  ngOnInit(): void {
    this.getAllUsers();
    this.getAllRoles();
  }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }

  getAllUsers() {
    this._apiService.get(`User/Getall`)
      .subscribe((response: any) => {
        this.userList = response.data;
        this.users = response.data;
      });
  }

  getAllRoles() {
    this._apiService.get(`Role/Getall`)
      .subscribe((response: any) => {
        this.roles = response.data
      });
  }

  editUser(user: UserDto): void {
    let userDetails: UserDto = Object.assign({}, JSON.parse(JSON.stringify(user)));
    this.userEdit = Object.assign({}, userDetails);
    // this.userEdit.managerId = user.managerId;
    // if (user.id != null && user.id != 0) {
    //   this.users = this.userList.filter(x => x.id != user.id);
    // }
    this.showDialog = true;
  }

  deleteUser(user: UserDto): void {
    if (user.id != null && user.id != 0) {
      this._apiService.delete(`User/Delete/${user.id}`)
        .subscribe((response: any) => {
          if (response.isSuccess) {
            this._toasterService.toastSuccess(response.message);
            this.getAllUsers();
          } else {
            this._toasterService.toastError(response.message);
          }
        });
    }
  }

  addUser(form: NgForm): void {
    form.reset();
    this.userEdit = <UserDto>{};
    this.showDialog = true;
  }

  onSubmit(form: NgForm) {
    let payload = {
      fullname: this.userEdit.fullName,
      email: this.userEdit.email,
      password: this.userEdit.password,
      phoneNumber: this.userEdit.phoneNumber,
      roleId: this.userEdit.roleId,
      managerId: this.userEdit.managerId,
      isActive: this.userEdit.isActive
    };

    if (this.userEdit.id != null && this.userEdit.id != 0) {
      this._apiService.put(`User/Update/${this.userEdit.id}`, payload).subscribe((response: any) => {
        if (response.isSuccess) {
          this._toasterService.toastSuccess(response.message);
          this.getAllUsers();
          this.hideDialog(form);
        } else {
          this._toasterService.toastError(response.message);
        }
      });
    } else {
      this._apiService.post(`User/Create`, payload).subscribe((response: any) => {
        if (response.isSuccess) {
          this._toasterService.toastSuccess(response.message);
          this.getAllUsers();
          this.hideDialog(form);
        } else {
          this._toasterService.toastError(response.message);
        }
      });
    }
  }

  hideDialog(form: NgForm) {
    form.reset();
    this.userEdit = <UserDto>{};
    this.showDialog = false;
  }

  deleteConfirm(event: Event, user: UserDto) {
    this._confirmationService.confirm({
      target: (<Element>event.target),
      message: 'Are you sure that you want to proceed?',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deleteUser(user);
      },
      reject: () => {
        //reject action
      }
    });
  }

}
