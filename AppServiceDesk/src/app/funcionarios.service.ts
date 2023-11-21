import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Funcionario } from './Funcionario';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class FuncionariosService {
  apiUrl = 'http://localhost:5000/api/Funcionario';

  constructor(private http: HttpClient) { }

  cadastrar(funcionario: Funcionario): Observable<Funcionario> { // <Object> ???
    const url = `${this.apiUrl}/cadastrar`;  // Correção aqui
    return this.http.post<Funcionario>(url, funcionario, httpOptions);
  }

  fazerLogin(funcionario: Funcionario): Observable<Funcionario> {
    const url = `${this.apiUrl}/fazerLogin`;
    return this.http.post<Funcionario>(url, funcionario, httpOptions)
  }

  atualizarPeril(funcionario: Funcionario): Observable<Funcionario> {
    const url = `${this.apiUrl}/atualizarPeril`;
    return this.http.put<Funcionario>(url, funcionario, httpOptions);
  }

  visualizarTickets(funcionarioId: Funcionario): Observable<Funcionario> {
    const url = `${this.apiUrl}/visualizarTickets/${funcionarioId}`;
    return this.http.get<Funcionario>(url)
  }

  listar(): Observable<Array<Funcionario>> {
    const url = `${this.apiUrl}/listar`;
    return this.http.get<Array<Funcionario>>(url);
  }

  alterar(id: any, funcionario: Funcionario): Observable<Funcionario> {
    const url = `${this.apiUrl}/alterar/${id}`;
    return this.http.put<Funcionario>(url, funcionario, httpOptions);
  }

  excluir(id: any): Observable<any> {
    const url = `${this.apiUrl}/excluir/${id}`;
    return this.http.delete<string>(url, httpOptions);
  }
}
