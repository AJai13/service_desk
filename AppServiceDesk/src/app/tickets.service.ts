import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ticket } from './Ticket';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class TicketsService {
  apiUrl = 'http://localhost:5000/Ticket';

  constructor(private http: HttpClient) {}

  criarTicket(ticket: Ticket): Observable<Ticket> {
    const url = `${this.apiUrl}/criarTicket`;
    return this.http.post<Ticket>(url, ticket, httpOptions);
  }
  atribuirTicket(ticket: Ticket): Observable<Ticket> {
    const url = `${this.apiUrl}/atribuirTicket`;
    return this.http.post<Ticket>(url, ticket, httpOptions);
  }

  getTicket(id: number): Observable<Ticket> {
    const url = `${this.apiUrl}/getTicket/${id}`;
    return this.http.get<Ticket>(url)
  }

  listar(): Observable<Array<Ticket>> {
    const url = `${this.apiUrl}/listarTickets`;
    return this.http.get<Array<Ticket>>(url);
  }

  alterar(id: any, ticket: Ticket): Observable<Ticket> {
    const url = `${this.apiUrl}/alterar/${id}`;
    return this.http.put<Ticket>(url, ticket, httpOptions);
  }

  excluir(id: any): Observable<any> {
    const url = `${this.apiUrl}/excluir/${id}`;
    return this.http.delete<string>(url, httpOptions);
  }
}
