import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { BooksComponent } from './book/books/books.component';
import { BookCreateComponent } from './book/book-create/book-create.component';
import { AuthorListComponent } from './author/author-list/author-list.component';
import { AuthorCreateComponent } from './author/author-create/author-create.component';
import { AuthorBooksComponent } from './author/author-books/author-books.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';

export const routes: Routes = [
    {path: 'books', component: BooksComponent},
    {path: 'books/:id/create', component: BookCreateComponent},
    {path: 'authors', component: AuthorListComponent},
    {path: 'authors/create', component: AuthorCreateComponent},
    {path: 'author/:id/books', component:AuthorBooksComponent},
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
    {path: '**', component: HomeComponent, pathMatch: 'full'}
];
