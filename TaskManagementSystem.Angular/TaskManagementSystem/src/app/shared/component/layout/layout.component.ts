import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { AuthenticationService } from '../../services/authentication.service';
import { LocalStorageService } from '../../services/local-storage.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  items: MenuItem[] = [];


  constructor(
    private _router: Router,
    private _authenticationService: AuthenticationService,
    private _localStorageService: LocalStorageService,
  ) {
    this.items = [
      {
        label: 'Home',
        icon: 'pi pi-home',
        command: () => {
          this._router.navigate([`home`]);
        }
      },
      {
        label: 'Report',
        icon: 'pi pi-bars',
        command: () => {
          this._router.navigate([`report`]);
        }
      }
    ];
    let user = this._authenticationService.getCurrentUser();
    if (user.Role == "Admin" || user.Role == "Manager") {
      this.items.push({
        label: 'Master',
        icon: 'pi pi-bars',
        items: [
          {
            label: 'Role',
            icon: 'pi pi-angle-right',
            command: () => {
              this._router.navigate([`master/role`]);
            }
          },
          {
            label: 'User',
            icon: 'pi pi-angle-right',
            command: () => {
              this._router.navigate([`master/user`]);
            }
          },
          {
            label: 'Project',
            icon: 'pi pi-angle-right',
            command: () => {
              this._router.navigate([`master/project`]);
            }
          },
          {
            label: 'Task',
            icon: 'pi pi-angle-right',
            command: () => {
              this._router.navigate([`master/task`]);
            }
          }
        ]
      });
    }
    this.items.push({
      label: 'Logout',
      icon: 'pi pi-sign-out',
      command: () => {
        this._localStorageService.deleteAllData();
        this._router.navigate([`sign-in`]);
      }
    });
  }

  ngOnInit(): void {
  }

}
