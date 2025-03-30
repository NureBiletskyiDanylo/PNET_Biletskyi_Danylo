import { Component, input } from '@angular/core';
import { Author } from '../../_models/author';

@Component({
  selector: 'app-author-card',
  standalone: true,
  imports: [],
  templateUrl: './author-card.component.html',
  styleUrl: './author-card.component.css'
})
export class AuthorCardComponent {
  author = input.required<Author>();
}
