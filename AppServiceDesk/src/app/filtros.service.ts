import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Filtro } from './Filtro';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class FiltrosService {
  apiUrl = 'http://localhost:5000';

  constructor(private http: HttpClient) {}

  setFiltro(filtro: Filtro): Observable<Filtro> { // <Object> ???
    const url = `${this.apiUrl}/setFiltro`;  // Correção aqui
    return this.http.post<Filtro>(url, filtro, httpOptions);
  }

  getFiltro(id: number): Observable<Filtro> {
    const url = `${this.apiUrl}/getFiltro/${id}`;
    return this.http.get<Filtro>(url)
  }

  listar(): Observable<Array<Filtro>> {
    const url = `${this.apiUrl}/listar`;
    return this.http.get<Array<Filtro>>(url);
  }

  associarFiltroAoTicket(filtro: Filtro): Observable<Filtro> {
    const url = `${this.apiUrl}/associarFiltroAoTicket`;
    return this.http.post<Filtro>(url, filtro, httpOptions)
  }

  alterar(id: any, filtro: Filtro): Observable<Filtro> {
    const url = `${this.apiUrl}/alterar/${id}`;
    return this.http.put<Filtro>(url, filtro, httpOptions);
  }

  excluir(id: any): Observable<any> {
    const url = `${this.apiUrl}/excluir/${id}`;
    return this.http.delete<string>(url, httpOptions);
  }
}
