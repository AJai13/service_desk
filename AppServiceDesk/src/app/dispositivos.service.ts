import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Dispositivo } from './Dispositivo';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class DispositivosService {
  apiUrl = 'http://localhost:5000/Dispositivo';

  constructor(private http: HttpClient) {}

  cadastrar(dispositivo: Dispositivo): Observable<Dispositivo> { // <Object> ???
    const url = `${this.apiUrl}/cadastrar`;  // Correção aqui
    return this.http.post<Dispositivo>(url, dispositivo, httpOptions);
  }

  buscar(id: number): Observable<Dispositivo> {
    const url = `${this.apiUrl}/buscar/${id}`;
    return this.http.get<Dispositivo>(url)
  }

  listar(): Observable<Array<Dispositivo>> {
    const url = `${this.apiUrl}/listar`;
    return this.http.get<Array<Dispositivo>>(url);
  }

  alterar(id: any, dispositivo: Dispositivo): Observable<Dispositivo> {
    const url = `${this.apiUrl}/alterar/${id}`;
    return this.http.put<Dispositivo>(url, dispositivo, httpOptions);
  }

  excluir(id: any): Observable<any> {
    const url = `${this.apiUrl}/excluir/${id}`;
    return this.http.delete<string>(url, httpOptions);
  }

  associarUsuarioAoDispositivo(dispositivoId: Dispositivo): Observable<Dispositivo> { // <Object> ???
    const url = `${this.apiUrl}/associarUsuarioAoDispositivo/${dispositivoId}`;  // Correção aqui
    return this.http.post<Dispositivo>(url, dispositivoId, httpOptions);
  }
}
