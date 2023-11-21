import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CentroDeCusto } from './CentroDeCusto';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class CentroDeCustosService {
  apiUrl = 'http://localhost:5000/api/CentroDeCusto';

  constructor(private http: HttpClient) { }

  listar(): Observable<Array<CentroDeCusto>> {
    const url = `${this.apiUrl}/listarCentrosDeCusto`;
    return this.http.get<Array<CentroDeCusto>>(url);
  }

  associarUsuarioAoCentroDeCusto(usuarioId: CentroDeCusto): Observable<CentroDeCusto> { // <Object> ???
    const url = `${this.apiUrl}/associarUsuarioAoCentroDeCusto/${usuarioId}`;  // Correção aqui
    return this.http.post<CentroDeCusto>(url, usuarioId, httpOptions);
  }

  associarCategoriaAoCentroDeCusto(categoriaId: CentroDeCusto): Observable<CentroDeCusto> { // <Object> ???
    const url = `${this.apiUrl}/associarCategoriaAoCentroDeCusto/${categoriaId}`;  // Correção aqui
    return this.http.post<CentroDeCusto>(url, categoriaId, httpOptions);
  }

  cadastrar(centroDeCusto: CentroDeCusto): Observable<CentroDeCusto> {
    const url = `${this.apiUrl}/associarCategoriaAoCentroDeCusto/`;  // Correção aqui
    return this.http.post<CentroDeCusto>(url, centroDeCusto, httpOptions);
  }

  excluir(id: any): Observable<any> {
    const url = `${this.apiUrl}/excluir/${id}`;
    return this.http.delete<string>(url, httpOptions);
  }

  alterar(id: any, centroDeCusto: CentroDeCusto): Observable<CentroDeCusto> {
    const url = `${this.apiUrl}/alterar/${id}`;
    return this.http.put<CentroDeCusto>(url, centroDeCusto, httpOptions);
  }

}
