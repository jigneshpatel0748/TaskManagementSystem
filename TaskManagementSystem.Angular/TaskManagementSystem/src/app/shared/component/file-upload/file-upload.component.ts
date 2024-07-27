import { HttpClient, HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent implements OnInit {

  @Input() type = "";
  @Output() public onUploadFinished = new EventEmitter();
  @Input() multiple: boolean = false;
  @Input() fileUrl: string = ""

  constructor(
    private _http: HttpClient
  ) { }

  ngOnInit(): void {
  }

  onFileUpload(event: any, fileUpload: any) {

    if (event.files.length === 0) {
      return;
    }

    let progress = 0;

    const formData = new FormData();

    let files = event.files

    for (var i = 0; i < files.length; i++) {
      formData.append('file', files[i], files[i].name);
    }

    this._http.post(`${environment.apiURL}/upload?type=${this.type}`, formData, { reportProgress: true, observe: 'events' })
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.Response) {
            fileUpload.clear();
            let output: any = event.body;
            this.fileUrl = output.join(',');
            this.onUploadFinished.emit(this.fileUrl);
          }
        },
        error: (err: HttpErrorResponse) => {
          fileUpload.clear();
          console.log(err)
        }
      });
  }

}
