import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Categoria } from './Categoria';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class CategoriasService {
  apiUrl = 'http://localhost:5000/Categoria';

  constructor(private http: HttpClient) {}

  setCategoria(categoria: Categoria): Observable<Categoria> { // <Object> ???
    const url = `${this.apiUrl}/setCategoria`;  // Correção aqui
    return this.http.post<Categoria>(url, categoria, httpOptions);
  }

  getCategoria(id: number): Observable<Categoria> {
    const url = `${this.apiUrl}/getCategoria/${id}`;
    return this.http.get<Categoria>(url)
  }

  listar(): Observable<Array<Categoria>> {
    const url = `${this.apiUrl}/listar`;
    return this.http.get<Array<Categoria>>(url);
  }

  associarCategoriaAoTicket(categoria: Categoria): Observable<Categoria> {
    const url = `${this.apiUrl}/associarCategoriaAoTicket`;
    return this.http.post<Categoria>(url, categoria, httpOptions)
  }

  excluir(id: any): Observable<any> {
    const url = `${this.apiUrl}/excluir/${id}`;
    return this.http.delete<string>(url, httpOptions);
  }

  alterar(id: any, categoria: Categoria): Observable<Categoria> {
    const url = `${this.apiUrl}/alterar/${id}`;
    return this.http.put<Categoria>(url, categoria, httpOptions);
  }
}


//html -> component -> service/model -> api
