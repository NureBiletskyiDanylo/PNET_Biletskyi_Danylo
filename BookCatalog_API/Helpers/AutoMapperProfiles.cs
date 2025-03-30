using AutoMapper;
using BookCatalog_API.DTOs;
using BookCatalog_API.Entities;

namespace BookCatalog_API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Author, AuthorCreateDto>(); 
        CreateMap<AuthorCreateDto, Author>();
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorDto, Author>()
            .ForMember(b => b.Books, opt => opt.Ignore());
        CreateMap<BookCreateDto, Book>();
        CreateMap<Book, BookDto>();
        CreateMap<GenreCreateDto, Genre>();
        CreateMap<GenreDto, Genre>()
            .ForMember(b => b.BooksWithGenre, opt => opt.Ignore());
        CreateMap<Genre, GenreDto>();
    }
}
