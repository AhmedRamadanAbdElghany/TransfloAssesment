import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { IDriver } from '../Models/idriver';
import { Observable } from 'rxjs';
import { IPaginationModel } from '../Models/i-pagination-model';

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  baseUrl: string = "";
  constructor(
    private http: HttpClient) {

   this.baseUrl = environment.baseurl
  }

  getDriver(id: number): Observable<any> {
    const url = `${this.baseUrl}/Driver/${id}`;
    return this.http.get<any>(url);
  }
}
