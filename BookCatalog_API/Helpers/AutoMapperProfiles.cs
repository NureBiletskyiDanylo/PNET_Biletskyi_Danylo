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
        CreateMap<BookCreateDto, Book>()
            .ForMember(b => b.BookGenres, opt => opt.Ignore())
            .ForMember(b => b.PublicationYear, opt => opt.MapFrom(dto => dto.PublicationDate));
        CreateMap<Book, BookCreateDto>()
            .ForMember(dto => dto.PublicationDate, opt => opt.MapFrom(b => b.PublicationYear));
        CreateMap<Book, BookDto>()
            .ForMember(dto => dto.PublicationDate, opt => opt.MapFrom(b => b.PublicationYear))
            .ForMember(dto => dto.BookGenres, opt =>
            opt.MapFrom(src => src.BookGenres.Select(bg => bg.Genre)));
        CreateMap<Book, BookEditDto>()
            .ForMember(dest => dest.BookGenres, opt => 
            opt.MapFrom(src => src.BookGenres.Select(bg => bg.Genre)))
            .ForMember(d => d.PublicationDate, opt => opt.MapFrom(src => src.PublicationYear));
        CreateMap<GenreCreateDto, Genre>();
        CreateMap<GenreDto, Genre>()
            .ForMember(b => b.BooksWithGenre, opt => opt.Ignore());
        CreateMap<Genre, GenreDto>();
        CreateMap<User, MemberDto>();
        CreateMap<BookCreateLog, BookLogsDto>();
    }
}
