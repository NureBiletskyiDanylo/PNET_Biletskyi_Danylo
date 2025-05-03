import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { BookCardComponent } from '../../book/book-card/book-card.component';
import { Favourite } from '../../_models/favourite';

@Component({
  selector: 'app-favourites',
  standalone: true,
  imports: [BookCardComponent],
  templateUrl: './favourites.component.html',
  styleUrl: './favourites.component.css'
})
export class FavouritesComponent implements OnInit{
  private accountService = inject(AccountService);
  favourites: Favourite = {books: []};

  ngOnInit(): void {
   this.loadFavourites(); 
  }

  loadFavourites(){
    this.accountService.getFavourites().subscribe({
      next: (data) => {
        this.favourites = data
      },
      error: (err) => {
        console.error("Error:", err);
      }
    })
  }

  removeBookFromFavourites(bookId: number){
    this.favourites.books  = this.favourites.books.filter(b => b.id !== bookId);
  }
}
