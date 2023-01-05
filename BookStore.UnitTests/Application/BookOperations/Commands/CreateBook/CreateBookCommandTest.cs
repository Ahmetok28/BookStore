using AutoMapper;
using BookStore.Application.BookOperations.Command.CreateBook;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using static BookStore.Application.BookOperations.Command.CreateBook.CreateBooksCommand;

namespace BookStore.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGıven_InvalidOperationExeption_ShouldBeReturn()
        {
            //arrange
            var Book = new Book() { Title = "Test_WhenAlreadyExistBookTitleIsGıven_InvalidOperationExeption_ShouldBeReturn", PageCount = 123, PublishDate = new System.DateTime(1999, 09, 07), AuthorId=1,GenreId=1 };
            _context.Books.Add(Book);
            _context.SaveChanges();

            CreateBooksCommand command= new CreateBooksCommand(_context,_mapper);
            command.Model =new CreateBookModel() { Title =Book.Title};

            //act && assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Zaten Ekli");

            //assert
        }
        public void WhenValidInputAreGıven_Book_ShouldBeCreated()
        {
            //arrange
            CreateBooksCommand command = new CreateBooksCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel() { Title = "Hobbit", PageCount = 123, PublishDate = new DateTime(1999, 09, 07), AuthorId = 1, GenreId = 1 };
            command.Model = model;
            

            

            //act 
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
                

            //assert
            var book = _context.Books.SingleOrDefault(book=>book.Title == model.Title);

            book.Should().NotBeNull();
            book.AuthorId.Should().Be(model.AuthorId);
            book.GenreId.Should().Be(model.GenreId);
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);    
        }
    }
}
