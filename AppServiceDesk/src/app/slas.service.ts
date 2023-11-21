import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Sla } from './Sla';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class SlasService {
  apiUrl = 'http://localhost:5000/SLA';

  constructor(private http: HttpClient) {}

  setSla(sla: Sla): Observable<Sla> { // <Object> ???
    const url = `${this.apiUrl}/setSla`;  // Correção aqui
    return this.http.post<Sla>(url, sla, httpOptions);
  }

  getSla(id: number): Observable<Sla> {
    const url = `${this.apiUrl}/getSla/${id}`;
    return this.http.get<Sla>(url)
  }

  listar(): Observable<Array<Sla>> {
    const url = `${this.apiUrl}/listar`;
    return this.http.get<Array<Sla>>(url);
  }

  associarSlaAoTicket(sla: Sla): Observable<Sla> {
    const url = `${this.apiUrl}/associarSlaAoTicket`;
    return this.http.post<Sla>(url, sla, httpOptions)
  }

  alterar(id: any, sla: Sla): Observable<Sla> {
    const url = `${this.apiUrl}/alterar/${id}`;
    return this.http.put<Sla>(url, sla, httpOptions);
  }

  excluir(id: any): Observable<any> {
    const url = `${this.apiUrl}/excluir/${id}`;
    return this.http.delete<string>(url, httpOptions);
  }
}
