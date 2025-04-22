import { Component, ElementRef, EventEmitter, inject, OnInit, Output, ViewChild } from '@angular/core';
import { Genre } from '../../_models/genre';
import { GenreService } from '../../_services/genre.service';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-genre-list',
  standalone: true,
  imports: [NgClass],
  templateUrl: './genre-list.component.html',
  styleUrl: './genre-list.component.css'
})
export class GenreListComponent implements OnInit{
  @ViewChild('selectedGenresString') selectedGenresInput!: ElementRef<HTMLInputElement>;
  focused:boolean = false;
  genres:Genre[] = [];
  selectedGenres: Genre[] = [];
  @Output() selectedGenresChange = new EventEmitter<Genre[]>();
  private genreService = inject(GenreService);
  switchList(){
    this.focused = !this.focused;
  }

  ngOnInit(): void {
    this.loadGenres();
  }

  loadGenres(){
    this.genreService.getGenres().subscribe({
      next: genres => this.genres = genres
    });
  }

  selectGenre(genre:Genre) {
    const index = this.selectedGenres.findIndex(g => g.id === genre.id);
    if (index === -1) {
      this.selectedGenres.push(genre);
    } else {
      this.selectedGenres.splice(index, 1);
    }
    this.buildSelectedString();
    this.selectedGenresChange.emit(this.selectedGenres);
  }

  addGenre(){
    var title = prompt("Please enter the genre name", "not-named");

    if (title){
      this.genreService.addGenre(title).subscribe({
        next: (genre: Genre) => {
          console.log('Genre added:', genre);
          this.genres.push(genre);
        },
        error: (err) => {
          console.error('Error: ', err);
        },
      });
    } 
  }

  isSelected(genre: Genre) : boolean{
    return this.selectedGenres.some(g => g.id === genre.id);
  }

  buildSelectedString(){
    var selectedGenresString;
    if (this.selectedGenres.length > 0){
      selectedGenresString = this.selectedGenres.map(genre => genre.name).join(', '); 
      this.selectedGenresInput.nativeElement.value = selectedGenresString;
      return;
    }
    this.selectedGenresInput.nativeElement.value = "";
  }
}
