import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { GenreListComponent } from '../../genre/genre-list/genre-list.component';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../_services/book.service';
import { Genre } from '../../_models/genre';
import { BookEdit } from '../../_models/bookEdit';

@Component({
  selector: 'app-book-edit',
  standalone: true,
  imports: [ReactiveFormsModule, BsDatepickerModule, GenreListComponent],
  templateUrl: './book-edit.component.html',
  styleUrl: './book-edit.component.css'
})
export class BookEditComponent implements OnInit{
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private bookService = inject(BookService);
  private authorId = -1;
  selectedGenres: Genre[] = [];
  bookForm = new FormGroup({
    title: new FormControl('', [
      Validators.required
    ]),
    description: new FormControl('', [
      Validators.maxLength(500)
    ]),
    publicationDate: new FormControl(<Date | null>(null), [
      Validators.required
    ])
  })
  id: number = 0;
  bsValue = new Date();

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.bookService.getBookById(this.id).subscribe({
      next: (book) => {
        const parts = book.publicationDate.split('-'); // "yyyy-MM-dd"
        const publicationDate = new Date(Number(parts[0]), Number(parts[1]) - 1, Number(parts[2]));

        this.bookForm.patchValue({
          title: book.title,
          description: book.description,
          publicationDate: publicationDate 
        });
        this.selectedGenres = book.bookGenres;
      },
      error: (err) => {
        console.error("Failed to load book", err);
      }
    });

    const authorId = history.state['authorsId'];
    console.log(authorId)
    if (authorId){
      this.authorId = authorId;
    }
  }

  handleSelectedGenreChange(genres: Genre[]){
    this.selectedGenres = genres;
  }
  editBook(){
    if (this.bookForm.valid){
      const bookToUpdate: BookEdit = {
        id: this.id,
        title: this.bookForm.value.title!,
        description: this.bookForm.value.description!,
        publicationDate: this.bookForm.value.publicationDate
            ? (this.bookForm.value.publicationDate as Date).toISOString().split('T')[0] 
    : '', // Convert Date to YYYY-MM-DD format
        bookGenres: this.selectedGenres
      };
      console.log(bookToUpdate);
      this.bookService.editBook(bookToUpdate).subscribe({
        next: () => {
          console.log(this.authorId);
          if (this.authorId === -1){
            this.router.navigate(['/books']);
          } else {
            this.router.navigate([`/author/${this.authorId}/books`])
          }
        },
        error: (err) => {
          console.error("Failed to update book", err);
        }
      })
    }
  }
}
