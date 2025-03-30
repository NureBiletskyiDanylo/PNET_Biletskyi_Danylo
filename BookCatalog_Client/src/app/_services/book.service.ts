import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Book } from '../_models/book';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;
  getBooks(){
    return this.http.get<Book[]>(`${this.baseUrl}/book/books`);
  }

  addBook(book: Book){
    // return this.http.post(`${this.baseUrl}`)
  }
  constructor() { }
}
