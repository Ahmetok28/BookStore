using AutoMapper;
using static BookStore.BookOperations.CreateBook.CreateBooksCommand;
using static BookStore.BookOperations.GetBooks.GetBookByIdQuery;
using static BookStore.BookOperations.GetBooks.GetBooksQuery;

namespace BookStore.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookViewModelById>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BookViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
            
        }
    }
}
