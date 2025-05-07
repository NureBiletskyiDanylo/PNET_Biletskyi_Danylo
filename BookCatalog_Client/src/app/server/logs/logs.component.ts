import { Component, inject, OnInit } from '@angular/core';
import { LoggingService } from '../../_services/logging.service';
import { Log } from '../../_models/log';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-logs',
  standalone: true,
  imports: [NgFor, NgIf],
  templateUrl: './logs.component.html',
  styleUrl: './logs.component.css'
})
export class LogsComponent implements OnInit{
  private loggingService = inject(LoggingService);
  logs: Log[] = [];

  ngOnInit(): void {
    this.loggingService.getLogs().subscribe({
      next: (response) => this.logs = response,
      error: (err) => console.error(err)
    });
  }
}
