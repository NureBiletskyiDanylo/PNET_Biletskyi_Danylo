import { Component, EventEmitter, inject, input, Output } from '@angular/core';
import { Book } from '../../_models/book';
import { BookService } from '../../_services/book.service';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-book-card',
  standalone: true,
  imports: [NgClass],
  templateUrl: './book-card.component.html',
  styleUrl: './book-card.component.css'
})
export class BookCardComponent {
  book = input.required<Book>();
  private bookService = inject(BookService);

  @Output() bookDeleted = new EventEmitter<number>();

  deleteBook(){
    var confirmed = confirm("Do you really want to delete the book?");
    if (confirmed){
      this.bookService.deleteBook(this.book().id).subscribe({
        next: () => {
          console.log("Done");
          this.bookDeleted.emit(this.book().id);
        }
      });
    }
  }

  makeFavourite(){
    if (this.book()){
      this.bookService.makeFavourite(this.book().id)
      .subscribe({
        next: (isFavourite: boolean) => {
          this.book().isFavourite = isFavourite;
        }
      });
    }
  }
}
