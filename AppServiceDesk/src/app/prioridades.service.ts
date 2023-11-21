import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Prioridade } from './Prioridade';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class PrioridadesService {
  apiUrl = 'http://localhost:5000/Prioridade';

  constructor(private http: HttpClient) {}

  setPrioridade(prioridade: Prioridade): Observable<Prioridade> { // <Object> ???
    const url = `${this.apiUrl}/setPrioridade`;  // Correção aqui
    return this.http.post<Prioridade>(url, prioridade, httpOptions);
  }

  getPrioridade(id: number): Observable<Prioridade> {
    const url = `${this.apiUrl}/getPrioridade/${id}`;
    return this.http.get<Prioridade>(url)
  }

  listar(): Observable<Array<Prioridade>> {
    const url = `${this.apiUrl}/listar`;
    return this.http.get<Array<Prioridade>>(url);
  }

  associarPrioridadeAoTicket(prioridade: Prioridade): Observable<Prioridade> {
    const url = `${this.apiUrl}/associarPrioridadeAoTicket`;
    return this.http.post<Prioridade>(url, prioridade, httpOptions)
  }

  alterar(id: any, prioridade: Prioridade): Observable<Prioridade> {
    const url = `${this.apiUrl}/alterar/${id}`;
    return this.http.put<Prioridade>(url, prioridade, httpOptions);
  }

  excluir(id: any): Observable<any> {
    const url = `${this.apiUrl}/excluir/${id}`;
    return this.http.delete<string>(url, httpOptions);
  }
}
