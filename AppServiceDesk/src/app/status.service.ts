import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Status } from './Status';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class StatusService {
  apiUrl = 'http://localhost:5000/Status';

  constructor(private http: HttpClient) { }

  setStatus(status: Status): Observable<Status> { // <Object> ???
    const url = `${this.apiUrl}/setStatus`;  // Correção aqui
    return this.http.post<Status>(url, status, httpOptions);
  }

  getStatus(id: number): Observable<Status> {
    const url = `${this.apiUrl}/getStatus/${id}`;
    return this.http.get<Status>(url)
  }

  listar(): Observable<Array<Status>> {
    const url = `${this.apiUrl}/listar`;
    return this.http.get<Array<Status>>(url);
  }

  associarStatusAoTicket(status: Status): Observable<Status> {
    const url = `${this.apiUrl}/associarStatusAoTicket`;
    return this.http.post<Status>(url, status, httpOptions)
  }

  alterar(id: any, status: Status): Observable<Status> {
    const url = `${this.apiUrl}/alterar/${id}`;
    return this.http.put<Status>(url, status, httpOptions);
  }

  excluir(id: any): Observable<any> {
    const url = `${this.apiUrl}/excluir/${id}`;
    return this.http.delete<string>(url, httpOptions);
  }
}


//html -> component -> service/model -> api
