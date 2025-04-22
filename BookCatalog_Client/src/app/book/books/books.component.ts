import { Component, inject, OnInit } from '@angular/core';
import { BookCardComponent } from "../book-card/book-card.component";
import { Book } from '../../_models/book';
import { BookService } from '../../_services/book.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [BookCardComponent, RouterLink],
  templateUrl: './books.component.html',
  styleUrl: './books.component.css'
})
export class BooksComponent implements OnInit{
  ngOnInit(): void {
    this.loadBooks();
  }
  private bookService = inject(BookService);
  books: Book[] = [];

  loadBooks(){
    this.bookService.getBooks().subscribe({
      next: (data) => {
        this.books = data;
      },
      error: (err) => {
        console.error("Error:", err);
      }
    })
  }

  handleBookDeleted(bookId: number){
    this.books = this.books.filter(book => book.id != bookId);
  }
}
