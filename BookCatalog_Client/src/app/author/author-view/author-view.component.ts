import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorService } from '../../_services/author.service';
import { Author } from '../../_models/author';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-author-view',
  standalone: true,
  imports: [NgFor, NgIf],
  templateUrl: './author-view.component.html',
  styleUrl: './author-view.component.css'
})
export class AuthorViewComponent implements OnInit{
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private authorService = inject(AuthorService);

  author!: Author;

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.authorService.getAuthorById(id).subscribe({
      next: (author) => {
        this.author = author;
      },
      error: (err) => {
        console.error("Failed to load author", err);
      }
    });
  }

  goBack(): void{
    history.back();
  }

  viewBook(bookId: number) : void{
    this.router.navigate(['/books',bookId, 'view']);
  }
}
