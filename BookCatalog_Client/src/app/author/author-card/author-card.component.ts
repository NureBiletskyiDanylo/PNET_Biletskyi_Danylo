import { Component, inject, input } from '@angular/core';
import { Author } from '../../_models/author';
import { Router, RouterLink } from '@angular/router';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-author-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './author-card.component.html',
  styleUrl: './author-card.component.css'
})
export class AuthorCardComponent {
  private router = inject(Router);
  accountService = inject(AccountService);
  author = input.required<Author>();

  viewAuthor(id: number){
    this.router.navigate(['/authors', id, 'view']);
  }
}
