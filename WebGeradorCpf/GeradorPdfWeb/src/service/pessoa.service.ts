import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pessoa } from '../inferface/Pessoa';



@Injectable({
  providedIn: 'root',
})
export class PessoaService {
  private api = 'https://localhost:7240/api/Pessoas';

  constructor(private http: HttpClient) {}

  obterTodasPessoa(): Observable<Pessoa[]> {
    return this.http.get<Pessoa[]>(`${this.api}`);
  }

  deletarPessoa(id: number): Observable<void> {
    return this.http.delete<void>(`${this.api}/${id}`);
  }

  obterPessoaPorId(id: number): Observable<Pessoa> {
    return this.http.get<Pessoa>(`${this.api}/${id}`);
  }

  criarPessoa(pessoa: Pessoa): Observable<Pessoa> {
    return this.http.post<Pessoa>(`${this.api}`, pessoa);
  }

  atualizarPessoa(id: number, pessoa: Pessoa): Observable<Pessoa> {
    return this.http.put<Pessoa>(`${this.api}/${id}`, pessoa);
  }

baixarPdf(): Observable<Blob> {
    return this.http.get(`${this.api}/baixar-pdf`, { responseType: 'blob' });
  }
  baixarPdfPessoa(id: number): Observable<any> {
    return this.http.get(`${this.api}/${id}/gerar-pdf`, { responseType: 'blob' });
  }

}
