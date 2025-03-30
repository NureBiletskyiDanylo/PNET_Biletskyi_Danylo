import { Component, inject } from '@angular/core';
import { AuthorService } from '../../_services/author.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateAuthor } from '../../_models/createAuthor';
import { Router } from '@angular/router';

@Component({
  selector: 'app-author-create',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './author-create.component.html',
  styleUrl: './author-create.component.css'
})
export class AuthorCreateComponent {
  private authorService = inject(AuthorService);
  private router = inject(Router);
  authorForm = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.maxLength(50)
    ]),
    authorInfo: new FormControl('',
      Validators.maxLength(500)
    )
  });


  createAuthor(){
    const author: CreateAuthor = {
      name: this.authorForm.value.name!,
      authorInfo: this.authorForm.value.authorInfo ?? '' // Set default empty string if null
    };
    this.authorService.createAuthor(author).subscribe({
      next: (response) => {
        console.log('Author created', response);
        this.router.navigate(['/authors'])
      },
      error: (error) => {
        console.error("Error:", error);
      }
    });
  }
}
