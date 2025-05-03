import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Author } from '../_models/author';
import { CreateAuthor } from '../_models/createAuthor';
import { AuthorEdit } from '../_models/authorEdit';

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
  
  getAuthorForEditById(id:number){
    return this.http.get<AuthorEdit>(`${this.baseUrl}/author/author/${id}`);
  }

  createAuthor(author: CreateAuthor){
    return this.http.post(`${this.baseUrl}/author/author/create`, author);
  }

  updateAuthor(author: AuthorEdit) {
    return this.http.put(`${this.baseUrl}/author/author/${author.id}`, author);
  }

  constructor() { }
}
