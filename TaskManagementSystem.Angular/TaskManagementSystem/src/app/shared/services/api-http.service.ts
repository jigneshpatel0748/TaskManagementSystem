import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable()
export class ApiHttpService {
    baseUrl: string = "";
    constructor(
        private http: HttpClient
    ) {
        this.baseUrl = environment.apiURL;
    }
    public get(url: string, options?: any) {
        return this.http.get(`${this.baseUrl}/${url}`, options);
    }
    public post(url: string, data: any, options?: any) {
        return this.http.post(`${this.baseUrl}/${url}`, data, options);
    }
    public put(url: string, data: any, options?: any) {
        return this.http.put(`${this.baseUrl}/${url}`, data, options);
    }
    public delete(url: string, options?: any) {
        return this.http.delete(`${this.baseUrl}/${url}`, options);
    }
}