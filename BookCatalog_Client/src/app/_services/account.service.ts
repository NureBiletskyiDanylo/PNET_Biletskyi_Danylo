import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { User } from '../_models/user';
import { map, of } from 'rxjs';
import { Favourite } from '../_models/favourite';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject (HttpClient);
  private router = inject(Router);
  baseUrl = environment.apiUrl;
  currentUser = signal<User | null>(null);

  login(model: any){
    return this.http.post<User>(this.baseUrl + '/account/login', model).pipe(
      map(user => {
        if (user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    )
  }

  register(model: any){
    return this.http.post<User>(this.baseUrl + '/account/register', model).pipe(
      map(user => {
        if (user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    )
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }

  getFavourites(){
      return this.http.get<Favourite>(this.baseUrl + '/book/favourites');
  }
}
