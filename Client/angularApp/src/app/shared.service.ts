import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
// observable is used to handle async request

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  readonly APIUrl = 'http://localhost:58983/api';
  readonly PhotoUrl = 'http://localhost:58983/Photos';
  constructor(private http: HttpClient) {}

  getDepList(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/department');
  }

  addDepartment(value: any) {
    return this.http.post(this.APIUrl + '/department', value);
  }

  updateDepartment(value: any) {
    return this.http.put(this.APIUrl + '/department', value);
  }

  deleteDepartment(value: any) {
    return this.http.delete(this.APIUrl + '/department/' + value);
  }

  getEmpList(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/employee');
  }

  addEmployee(value: any) {
    return this.http.post(this.APIUrl + '/employee', value);
  }

  updateEmployee(value: any) {
    return this.http.put(this.APIUrl + '/employee', value);
  }

  deleteEmployee(value: any) {
    return this.http.delete(this.APIUrl + '/employee/' + value);
  }

  uploadPhoto(value: any) {
    return this.http.post(this.APIUrl + '/employee/SaveFile', value);
  }

  // observable to get, because we fetch data from the database
  // so we return a json with that data, an array
  getAllDepartmentNames(): Observable<any[]> {
    return this.http.get<any[]>(
      this.APIUrl + '/employee/GetAllDepartmentNames'
    );
  }
}
