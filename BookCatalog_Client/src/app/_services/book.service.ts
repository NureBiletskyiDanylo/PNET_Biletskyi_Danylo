import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Book } from '../_models/book';
import { environment } from '../../environments/environment';
import { CreateBook } from '../_models/createBook';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;
  getBooks(){
    return this.http.get<Book[]>(`${this.baseUrl}/book/books`);
  }

  getBooksByAuthorId(id:number){
    return this.http.get<Book[]>(`${this.baseUrl}/book/books/${id}`)
  }

  addBook(id:number, book: CreateBook){
    return this.http.post(`${this.baseUrl}/book/book/create/${id}`, book);
  }

  deleteBook(id:number){
    return this.http.delete(`${this.baseUrl}/book/books/${id}`);
  }

  makeFavourite(id:number){
    return this.http.post<boolean>(`${this.baseUrl}/book/books/favourite/${id}`, {});
  }
  constructor() { }
}
