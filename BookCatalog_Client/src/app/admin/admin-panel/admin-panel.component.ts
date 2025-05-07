import { Component, inject, OnInit } from '@angular/core';
import { UserService } from '../../_services/user.service';
import { Member } from '../../_models/member';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css'
})
export class AdminPanelComponent implements OnInit{
  goToBookLogs() {
    this.router.navigate(['/admin/book-logs'])
  }
  
  goToServerLogs(){
    this.router.navigate(['/admin/server-logs']);
  }
  private router = inject(Router);
  private userService = inject(UserService);

  members!: Member[];
  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
      this.userService.getMembers().subscribe({
      next: (members) => this.members = members,
      error: (err) => console.error("Failed to load users", err)
    });
  }

  changeRole(userId: number, newRole: string) {
    this.userService.updateUserRole(userId, newRole).subscribe({
      next: () => console.log(`Updated role for user ${userId} to ${newRole}`),
      error: (err) => console.error(`Failed to update role`, err)
    });
  }

  deleteMember(userId: number) {
    if (confirm('Are you sure you want to delete this user?')) {
      this.userService.deleteUser(userId).subscribe({
        next: () => {
          this.members = this.members.filter(m => m.id !== userId);
          console.log(`User ${userId} deleted`);
        },
        error: (err) => console.error(`Failed to delete user`, err)
      });
    }
  }

}
