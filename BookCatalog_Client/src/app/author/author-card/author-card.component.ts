import { Component, input } from '@angular/core';
import { Author } from '../../_models/author';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-author-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './author-card.component.html',
  styleUrl: './author-card.component.css'
})
export class AuthorCardComponent {
  author = input.required<Author>();
}
