import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { MenubarModule } from 'primeng/menubar';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { LayoutComponent } from './component/layout/layout.component';
import { CalendarModule } from 'primeng/calendar';
import { AccordionModule } from 'primeng/accordion';
import { BadgeModule } from 'primeng/badge';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FileUploadComponent } from './component/file-upload/file-upload.component';
import { FileUploadModule } from 'primeng/fileupload';


@NgModule({
  declarations: [
    LayoutComponent,
    FileUploadComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ToastModule,
    MenubarModule,
    ConfirmPopupModule,
    TableModule,
    InputTextModule,
    CheckboxModule,
    ButtonModule,
    DialogModule,
    DropdownModule,
    CalendarModule,
    AccordionModule,
    BadgeModule,
    InputTextareaModule,
    FileUploadModule
  ],
  exports: [
    FormsModule,
    ToastModule,
    MenubarModule,
    ConfirmPopupModule,
    TableModule,
    InputTextModule,
    CheckboxModule,
    ButtonModule,
    CommonModule,
    DialogModule,
    DropdownModule,
    CalendarModule,
    AccordionModule,
    BadgeModule,
    InputTextareaModule,
    FileUploadModule,
    FileUploadComponent
  ]
})
export class SharedModule { }
