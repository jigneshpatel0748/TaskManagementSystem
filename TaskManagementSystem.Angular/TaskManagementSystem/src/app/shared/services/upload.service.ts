import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
@Injectable({
    providedIn: 'root'
})
export class UploadService {

    baseApiUrl = environment.apiURL;

    constructor(private http: HttpClient) { }

    upload(type: string, file: any): Observable<any> {
        const formData = new FormData();
        formData.append("file", file, file.name);
        return this.http.post(`${this.baseApiUrl}/api/Upload?type=${type}`, formData)
    }
}
