import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  constructor() { }
  getMembers() {
    return this.http.get<Member[]>(`${this.baseUrl}/members/get-users`);
  }

  updateUserRole(id: number, role: string) {
    return this.http.put(`${this.baseUrl}/members/${id}/update-role`, {role});
  }

  deleteUser(id: number) {
    return this.http.delete(`${this.baseUrl}/members/${id}`);
  }
}
