import { Component, inject, OnInit } from '@angular/core';
import { BookService } from '../../_services/book.service';
import { BookLog } from '../../_models/bookLog';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-book-log',
  standalone: true,
  imports: [NgIf, NgFor],
  templateUrl: './book-log.component.html',
  styleUrl: './book-log.component.css'
})
export class BookLogComponent implements OnInit{
  private bookService = inject(BookService);
  bookLogs: BookLog[] = [];

  ngOnInit(): void {
    this.bookService.getBookLogs().subscribe({
      next: (response) => {
        this.bookLogs = response;
      },
      error: (err) => console.error("Failed to load book logs", err)
    })
  }
}
