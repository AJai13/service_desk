import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from './Usuario';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {
  apiUrl = 'http://localhost:5000/Usuario';

  constructor(private http: HttpClient) {}

  cadastrar(usuario: Usuario): Observable<Usuario> { // <Object> ???
    const url = `${this.apiUrl}/cadastrar`;  // Correção aqui
    return this.http.post<Usuario>(url, usuario, httpOptions);
  }

  buscar(email: string): Observable<Usuario> {
    const url = `${this.apiUrl}/buscar/${email}`;
    return this.http.get<Usuario>(url)
  }

  listar(): Observable<Array<Usuario>> {
    const url = `${this.apiUrl}/listarUsuarios`;
    return this.http.get<Array<Usuario>>(url);
  }

  alterar(id: any, usuario: Usuario): Observable<Usuario> {
    const url = `${this.apiUrl}/alterar/${id}`;
    return this.http.put<Usuario>(url, usuario, httpOptions);
  }

  excluir(id: any): Observable<any> {
    const url = `${this.apiUrl}/excluir/${id}`;
    return this.http.delete<string>(url, httpOptions);
  }
}
