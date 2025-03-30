import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { catchError, Observable, throwError } from 'rxjs';
import { Genre } from '../_models/genre';

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;


  getGenres(){
    return this.http.get<Genre[]>(`${this.baseUrl}/genre/genre/all`);
  }

  addGenre(name:string): Observable<Genre>{
    return this.http.post<Genre>(`${this.baseUrl}/genre/genre/create`, {Name : name}).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse){
    if (error.error instanceof ErrorEvent){
      console.error('Client-side error:', error.error.message);
      return throwError(() => new Error('Client-side error: ' + error.error.message));
    } else{
      console.error(`Server-side error: ${error.status} - ${error.message}`);
      return throwError(() => new Error(error.error || 'An error occured'));
    }
  }
  constructor() { }
}
