import { Component, EventEmitter, inject, Input, input, Output } from '@angular/core';
import { Book } from '../../_models/book';
import { BookService } from '../../_services/book.service';
import { NgClass } from '@angular/common';
import { Router } from '@angular/router';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-book-card',
  standalone: true,
  imports: [NgClass],
  templateUrl: './book-card.component.html',
  styleUrl: './book-card.component.css'
})
export class BookCardComponent {
  @Output() bookUnfavourited = new EventEmitter<number>();
  @Input() authorId: number = -1;
  book = input.required<Book>();
  private router = inject(Router);
  private bookService = inject(BookService);
  accountService = inject(AccountService);

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

          if (!isFavourite){
            this.bookUnfavourited.emit(this.book().id);
          }
        }
      });
    }
  }

  editBook(id:number){
    console.log(this.authorId);
    const authorsId = this.authorId;
    this.router.navigate(['/books', id, 'edit'], {
      state:{authorsId}
    });
  }

  viewBook(id: number){
    this.router.navigate(['/books', id, 'view']);
  }
}
