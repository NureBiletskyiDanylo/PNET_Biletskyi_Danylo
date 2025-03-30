import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Author } from '../_models/author';
import { CreateAuthor } from '../_models/createAuthor';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  getAuthors(){
    return this.http.get<Author[]>(`${this.baseUrl}/author/author`);
  }

  getAuthorById(id:number){
    return this.http.get<Author>(`${this.baseUrl}/author/author/${id}`);
  }

  createAuthor(author: CreateAuthor){
    return this.http.post(`${this.baseUrl}/author/author/create`, author);
  }

  constructor() { }
}
