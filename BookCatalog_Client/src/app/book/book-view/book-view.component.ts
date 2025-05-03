import { Component, inject, input, OnInit } from '@angular/core';
import { Book } from '../../_models/book';
import { DatePipe, NgFor, NgIf } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../_services/book.service';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-book-view',
  standalone: true,
  imports: [NgIf, NgFor],
  templateUrl: './book-view.component.html',
  styleUrl: './book-view.component.css'
})
export class BookViewComponent implements OnInit{
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private bookService = inject(BookService);
  accountService = inject(AccountService);
  book!: Book;

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.bookService.getBookByIdView(id).subscribe({
      next: (book) => {
        this.book = book
        console.log(book);
      },
      error: (err) => console.error('Failed to load book', err)
    });
  }
  goBack() {
    history.back();
  }
  editBook(arg0: any) {
    this.router.navigate(['books',this.book.id, 'edit']);
  }
}
