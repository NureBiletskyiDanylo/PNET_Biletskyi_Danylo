import { Component, inject, OnInit } from '@angular/core';
import {BsDatepickerConfig, BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { NgIf } from '@angular/common';
import { GenreListComponent } from "../../genre/genre-list/genre-list.component";
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateBook } from '../../_models/createBook';
import { Genre } from '../../_models/genre';
import { BookService } from '../../_services/book.service';

@Component({
  selector: 'app-book-create',
  standalone: true,
  imports: [BsDatepickerModule, GenreListComponent, ReactiveFormsModule],
  templateUrl: './book-create.component.html',
  styleUrl: './book-create.component.css'
})
export class BookCreateComponent implements OnInit{
  private route = inject(ActivatedRoute);
  private router = inject (Router);
  private bookService = inject(BookService);
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
  id:Number = 0;
  bsValue = new Date();

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
  }

  createBook(){
    console.log(this.bookForm.value);
    const book: CreateBook = {
      title: this.bookForm.value.title!,
      description: this.bookForm.value.description ?? '',
      publicationDate: this.bookForm.value.publicationDate 
    ? (this.bookForm.value.publicationDate as Date).toISOString().split('T')[0] 
    : '', // Convert Date to YYYY-MM-DD format

      bookGenres: this.selectedGenres
    };
    this.bookService.addBook(this.id.valueOf(), book).subscribe({
      next: (() => {
        this.router.navigate(['/author', this.id, 'books']);
      }) 
    });
  }

  handleSelectedGenreChange(genres: Genre[]){
    this.selectedGenres = genres;
  }
}
