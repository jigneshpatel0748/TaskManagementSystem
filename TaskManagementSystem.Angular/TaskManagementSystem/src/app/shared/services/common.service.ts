import { Injectable } from "@angular/core";
import { MessageService } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class commonService {

  priorities: any[] = [
    { id: 1, name: "Low" },
    { id: 2, name: "Medium" },
    { id: 3, name: "High" }
  ];
  types: any[] = [
    { id: 1, name: "New" },
    { id: 2, name: "Bug" }
  ];
  status: any[] = [
    { id: 1, name: "Idle" },
    { id: 2, name: "Running" },
    { id: 3, name: "Pending" },
    { id: 4, name: "Complete" },
    { id: 5, name: "Cancel" }
  ];

  roleAccess: any[] = [
    { id: 1, name: "Idle" },
    { id: 2, name: "Running" },
    { id: 3, name: "Pending" },
    { id: 4, name: "Complete" },
    { id: 5, name: "Cancel" }
  ];

  constructor() { }


}

export class ApiValidationResponse {
  responseCode: number | undefined;
  alertCode: number | undefined;
  messageTitle: string | undefined;
  messageDescription: string | undefined;
}
