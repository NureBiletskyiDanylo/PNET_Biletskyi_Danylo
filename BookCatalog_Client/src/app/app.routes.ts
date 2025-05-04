import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { BooksComponent } from './book/books/books.component';
import { BookCreateComponent } from './book/book-create/book-create.component';
import { AuthorListComponent } from './author/author-list/author-list.component';
import { AuthorCreateComponent } from './author/author-create/author-create.component';
import { AuthorBooksComponent } from './author/author-books/author-books.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { FavouritesComponent } from './favourite/favourites/favourites.component';
import { AuthorEditComponent } from './author/author-edit/author-edit.component';
import { BookEditComponent } from './book/book-edit/book-edit.component';
import { BookViewComponent } from './book/book-view/book-view.component';
import { AuthorViewComponent } from './author/author-view/author-view.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { authGuard } from './_guard/auth.guard';
import { adminGuard } from './_guard/admin.guard';
import { BookLogComponent } from './book/book-log/book-log.component';

export const routes: Routes = [
    {path: 'books', component: BooksComponent},
    {path: 'books/:id/create', component: BookCreateComponent, canActivate: [adminGuard]},
    {path: 'books/:id/edit', component: BookEditComponent, canActivate: [adminGuard]},
    {path: 'books/:id/view', component: BookViewComponent},
    {path: 'authors', component: AuthorListComponent},
    {path: 'authors/create', component: AuthorCreateComponent, canActivate: [adminGuard]},
    {path: 'authors/:id/edit', component: AuthorEditComponent, canActivate: [adminGuard]},
    {path: 'author/:id/books', component:AuthorBooksComponent},
    {path: 'authors/:id/view', component: AuthorViewComponent},
    {path: 'favourites', component:FavouritesComponent, canActivate: [authGuard]},
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'admin', component:AdminPanelComponent, canActivate: [adminGuard]},
    {path: 'admin/book-logs', component: BookLogComponent, canActivate: [adminGuard]},
    {path: '**', component: HomeComponent, pathMatch: 'full'}
];
