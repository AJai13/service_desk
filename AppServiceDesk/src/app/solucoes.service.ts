import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Solucao } from './Solucao';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class SolucoesService {
  apiUrl = 'http://localhost:5000/api/solucao';

  constructor(private http: HttpClient) {}

  setSolucao(solucao: Solucao): Observable<Solucao> { // <Object> ???
    const url = `${this.apiUrl}/setSolucao`;  // Correção aqui
    return this.http.post<Solucao>(url, solucao, httpOptions);
  }

  getSolucao(id: number): Observable<Solucao> {
    const url = `${this.apiUrl}/getSolucao/${id}`;
    return this.http.get<Solucao>(url)
  }

  listar(): Observable<Array<Solucao>> {
    const url = `${this.apiUrl}/listar`;
    return this.http.get<Array<Solucao>>(url);
  }

  associarSolucaoAoTicket(solucao: Solucao): Observable<Solucao> {
    const url = `${this.apiUrl}/associarSolucaoAoTicket`;
    return this.http.post<Solucao>(url, solucao, httpOptions)
  }

  alterar(id: any, solucao: Solucao): Observable<Solucao> {
    const url = `${this.apiUrl}/alterar/${id}`;
    return this.http.put<Solucao>(url, solucao, httpOptions);
  }

  excluir(id: any): Observable<any> {
    const url = `${this.apiUrl}/excluir/${id}`;
    return this.http.delete<string>(url, httpOptions);
  }
}
