import { Component, inject, OnInit } from '@angular/core';
import { BookService } from '../../_services/book.service';
import { Book } from '../../_models/book';
import { ActivatedRoute, Router } from '@angular/router';
import { BookCardComponent } from '../../book/book-card/book-card.component';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-author-books',
  standalone: true,
  imports: [BookCardComponent],
  templateUrl: './author-books.component.html',
  styleUrl: './author-books.component.css'
})
export class AuthorBooksComponent implements OnInit{
  private bookService = inject(BookService);
  accountSErvice = inject(AccountService);
  private route = inject(ActivatedRoute);
  private router = inject(Router)
  id: number = 0;
  books: Book[] = [];

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.loadBooks(this.id);
  }

  goToCreateBook(){
    this.router.navigate(['books/', this.id, 'create']);
  }
  loadBooks(id:number){
    this.bookService.getBooksByAuthorId(id).subscribe({
      next: books => this.books = books
    });
  }

  handleBookDeleted(bookId: number){
    this.books = this.books.filter(book => book.id != bookId);
  }
}
