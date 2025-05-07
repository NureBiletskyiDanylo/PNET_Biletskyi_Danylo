import { Component, EventEmitter, inject, input, Output } from '@angular/core';
import { Author } from '../../_models/author';
import { Router, RouterLink } from '@angular/router';
import { AccountService } from '../../_services/account.service';
import { AuthorService } from '../../_services/author.service';

@Component({
  selector: 'app-author-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './author-card.component.html',
  styleUrl: './author-card.component.css'
})
export class AuthorCardComponent {
  private authorService = inject(AuthorService);
  private router = inject(Router);
  accountService = inject(AccountService);
  author = input.required<Author>();

  @Output() authorDeleted = new EventEmitter<number>();
  viewAuthor(id: number){
    this.router.navigate(['/authors', id, 'view']);
  }

  deleteAuthor(){
    var confirmed = confirm("Do you really want to delete the author?");
    if (confirmed){
      this.authorService.deleteAuthor(this.author().id)
      .subscribe({
        next: () => {
          console.log("Done");
          this.authorDeleted.emit(this.author().id);
        }
      });
    }
  }
}
