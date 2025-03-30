import { Component, inject, OnInit } from '@angular/core';
import { AuthorService } from '../../_services/author.service';
import { Author } from '../../_models/author';
import { AuthorCardComponent } from "../author-card/author-card.component";
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-author-list',
  standalone: true,
  imports: [AuthorCardComponent, RouterLink],
  templateUrl: './author-list.component.html',
  styleUrl: './author-list.component.css'
})
export class AuthorListComponent implements OnInit{
  private authorService = inject(AuthorService);
  authors:Author[] = [];

  ngOnInit(): void {
    this.loadAuthors();
  }

  loadAuthors(){
    this.authorService.getAuthors().subscribe({
      next: (data) => {
        this.authors = data;
      },
      error: (err) =>{
        console.error("Error", err);
      }
    })
  }
}
