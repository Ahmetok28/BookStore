using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.Application.UserOperations.Command.CreateUser;
using BookStore.Entities;
using static BookStore.Application.AuthorOperations.Query.GetAuthors.GetAuthorQuery;
using static BookStore.Application.AuthorOperations.Query.GetAuthorsDetail.GetAuthorsDetailQuery;
using static BookStore.Application.BookOperations.Command.CreateBook.CreateBooksCommand;
using static BookStore.Application.BookOperations.Command.UpdateBooks.UpdateBooksCommand;
using static BookStore.Application.BookOperations.Queries.GetBookDetail.GetBookByIdQuery;
using static BookStore.Application.BookOperations.Queries.GetBooks.GetBooksQuery;
using static BookStore.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;
using static BookStore.Application.GenreOperations.Queries.GetGenresDetail.GetGenresDetailQuery;
using static BookStore.Application.UserOperations.Command.CreateUser.CreateUserCommand;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book Mapping
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookViewModelById>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(x => (x.Authors.Name + " " + x.Authors.SurName).ToLower()));

            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(x => (x.Authors.Name + " " + x.Authors.SurName).ToLower()));
            CreateMap<UpdateBookModel, Book>();
            CreateMap<Book, UpdateBookModel>();


            //Genre Mapping
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<UpdateGenreModel, Genre>();
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenresDetailViewModel>();


            // Author Mapping
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<UpdateAuthorModel, Author>();
            CreateMap<Author, AuthorViewModel>().ForMember(
                    dest => dest.BirthDate,
                    opt => opt.MapFrom(src => src.BirthDate.Date.ToString("dd/MM/yyyy"))
                );
            CreateMap<Author, AuthorDetailViewModel>().ForMember(
                    dest => dest.BirthDate,
                    opt => opt.MapFrom(src => src.BirthDate.Date.ToString("dd/MM/yyyy"))
                );

            //User Mapping
            CreateMap<CreateUserModel, User>();
           // CreateMap<UpdateUserModel, Genre>();
           


        }
    }
}
