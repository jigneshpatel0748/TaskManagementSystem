import { Injectable } from "@angular/core";
import { MessageService } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class ToasterService {

  constructor(private messageService: MessageService) { }

  toastSuccess(message: string, detail: string = '') {
    this.messageService.add({ severity: 'success', summary: message, detail: detail, life: 5000 });
  }

  toastError(message: string, detail: string = '') {
    this.messageService.add({ severity: 'error', summary: message, detail: detail, life: 5000 });
  }

  toastInfo(message: string, detail: string = '') {
    this.messageService.add({ severity: 'info', summary: message, detail: detail, life: 5000 });
  }

  toastWarning(message: string, detail: string = '') {
    this.messageService.add({ severity: 'warn', summary: message, detail: detail, life: 5000 });
  }
}