import { Component } from '@angular/core';
import {BsDatepickerConfig, BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { NgIf } from '@angular/common';
import { GenreListComponent } from "../../genre/genre-list/genre-list.component";

@Component({
  selector: 'app-book-create',
  standalone: true,
  imports: [BsDatepickerModule, GenreListComponent],
  templateUrl: './book-create.component.html',
  styleUrl: './book-create.component.css'
})
export class BookCreateComponent {
  bsValue = new Date();
}
