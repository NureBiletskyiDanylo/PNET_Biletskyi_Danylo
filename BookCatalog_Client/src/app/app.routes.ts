import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { BooksComponent } from './book/books/books.component';
import { BookCreateComponent } from './book/book-create/book-create.component';
import { AuthorListComponent } from './author/author-list/author-list.component';
import { AuthorCreateComponent } from './author/author-create/author-create.component';

export const routes: Routes = [
    {path: 'books', component: BooksComponent},
    {path: 'books/create', component: BookCreateComponent},
    {path: 'authors', component: AuthorListComponent},
    {path: 'authors/create', component: AuthorCreateComponent},
    {path: '**', component: HomeComponent, pathMatch: 'full'}
];
