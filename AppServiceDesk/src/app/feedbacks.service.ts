import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Feedback } from './Feedback';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class FeedbacksService {
  apiUrl = 'http://localhost:5000/api/Feedback';

  constructor(private http: HttpClient) {}

  setFeedbackAssociarASolucao(feedback: Feedback): Observable<Feedback> { // <Object> ???
    const url = `${this.apiUrl}/setFeedbackAssociarASolucao`;  // Correção aqui
    return this.http.post<Feedback>(url, feedback, httpOptions);
  }

  getFeedback(id: number): Observable<Feedback> {
    const url = `${this.apiUrl}/getFeedback/${id}`;
    return this.http.get<Feedback>(url)
  }

  listar(): Observable<Array<Feedback>> {
    const url = `${this.apiUrl}/listar`;
    return this.http.get<Array<Feedback>>(url);
  }

  alterar(id: any, feedback: Feedback): Observable<Feedback> {
    const url = `${this.apiUrl}/alterar/${id}`;
    return this.http.put<Feedback>(url, feedback, httpOptions);
  }

  excluir(id: any): Observable<any> {
    const url = `${this.apiUrl}/excluir/${id}`;
    return this.http.delete<string>(url, httpOptions);
  }
}
