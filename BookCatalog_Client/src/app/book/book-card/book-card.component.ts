import { Component, input } from '@angular/core';
import { Book } from '../../_models/book';

@Component({
  selector: 'app-book-card',
  standalone: true,
  imports: [],
  templateUrl: './book-card.component.html',
  styleUrl: './book-card.component.css'
})
export class BookCardComponent {
  book = input.required<Book>();
}
