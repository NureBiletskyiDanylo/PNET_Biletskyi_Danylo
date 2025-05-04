import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Book } from '../_models/book';
import { environment } from '../../environments/environment';
import { CreateBook } from '../_models/createBook';
import { BookEdit } from '../_models/bookEdit';
import { BookLog } from '../_models/bookLog';

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

  // FOR EDIT ONLY
  getBookById(id:number){
    return this.http.get<BookEdit>(`${this.baseUrl}/book/books/book/${id}`);
  }

  getBookByIdView(id:number){
    return this.http.get<Book>(`${this.baseUrl}/book/books/book/view/${id}`);
  }


  editBook(book:BookEdit){
    return this.http.put(`${this.baseUrl}/book/books/${book.id}`, book);
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

  getBookLogs(){
    return this.http.get<BookLog[]>(`${this.baseUrl}/book/books/logs`);
  }
  constructor() { }
}
