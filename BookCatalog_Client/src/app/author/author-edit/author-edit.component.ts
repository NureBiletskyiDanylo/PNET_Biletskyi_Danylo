import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthorService } from '../../_services/author.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Author } from '../../_models/author';
import { AuthorEdit } from '../../_models/authorEdit';

@Component({
  selector: 'app-author-edit',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './author-edit.component.html',
  styleUrl: './author-edit.component.css'
})
export class AuthorEditComponent implements OnInit{
  private authorService = inject(AuthorService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  authorId!: number;

  authorEditForm = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.maxLength(50)
    ]),
    authorInfo: new FormControl('',
      Validators.maxLength(500)
    )
  });

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id){
        this.authorId = +id
        this.loadAuthor(this.authorId);
      }
    })
  }
  loadAuthor(id: number): void{
    this.authorService.getAuthorForEditById(id).subscribe({
      next: (author: AuthorEdit) => {
        this.authorEditForm.patchValue({
          name: author.name,
          authorInfo: author.authorInfo || ''
        });
      },
      error: err => {
        console.error("Failed to load author", err);
      }
    })
  }
  editForm(): void{
    if (this.authorEditForm.valid){
      const updatedAuthor: AuthorEdit = {
        id: this.authorId,
        name: this.authorEditForm.value.name!,
        authorInfo: this.authorEditForm.value.authorInfo! || ''
      };

      this.authorService.updateAuthor(updatedAuthor).subscribe({
        next: (response) => {
          this.router.navigate(['/authors']);
        },
        error: (error) => {
          console.error("Error:", error);
        }
      });
    }
  }
}
