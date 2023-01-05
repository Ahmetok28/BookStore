using AutoMapper;
using BookStore.Application.BookOperations.Command.CreateBook;
using BookStore.Application.BookOperations.Command.DeleteBooks;
using BookStore.Application.BookOperations.Command.UpdateBooks;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookStore.Application.BookOperations.Command.CreateBook.CreateBooksCommand;
using static BookStore.Application.BookOperations.Command.UpdateBooks.UpdateBooksCommand;

namespace BookStore.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTest:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        // Database de girilen ID'ye ait veri yok ise çalışacak olan kod
        [Fact]
        public void WhenDataNoForId_InvalidOperationExeption_SholudBeReturn()
        {
            // Arrange
            UpdateBooksCommand command = new UpdateBooksCommand(_context,_mapper);
            // Act and Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Veri Tabanında Böyle bir Kitap Yok");
        }
       
        // Girilen veriler doğru ise çalışacak olan kod
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            //Arrange
            var book = new Book() 
            { Title = "Test", 
                AuthorId = 1,
                GenreId = 1, 
                PageCount = 546, 
                PublishDate = new System.DateTime(1999, 09, 07) };

            var klonBook = new Book()
            {
                Title = book.Title,
                AuthorId = book.AuthorId,
                GenreId = book.GenreId,
                PageCount = book.PageCount,
                PublishDate = book.PublishDate
            };

            _context.Books.Add(book);
            _context.SaveChanges();

            var cmd = new UpdateBooksCommand(_context,_mapper);
            cmd.BookId = book.Id;
            UpdateBookModel model = new UpdateBookModel() { Title = "Hobbit", PageCount = 123, PublishDate = new DateTime(1989, 08, 17), AuthorId = 2, GenreId = 2 };
            cmd.Model = model;

            //Act
            FluentActions
                .Invoking(() => cmd.Handle()).Invoke();//Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isime sahip bir kitap zaten mevcut.");


            // Assert
            var result = _context.Books.SingleOrDefault(x => x.Title == model.Title);

            result.Should().NotBeNull();
            result.AuthorId.Should().NotBe(klonBook.AuthorId);
            result.PageCount.Should().NotBe(klonBook.PageCount);
            result.Title.Should().NotBe(klonBook.Title);
            result.GenreId.Should().NotBe(klonBook.GenreId);
            result.PublishDate.Should().NotBe(klonBook.PublishDate);

           
        }

        // Girilen kitap ismi ile çakışan başka bir kitap varsa çalışacak olan kod
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationExeption_SholudBeReturn()
        {
           

            var Book = new Book() { Title = "Test", PageCount = 123, PublishDate = new System.DateTime(1999, 09, 07), AuthorId = 1, GenreId = 1 };
            _context.Books.Add(Book);
            _context.SaveChanges();

            UpdateBooksCommand command = new UpdateBooksCommand(_context, _mapper);
            command.BookId = Book.Id;
            UpdateBookModel model = new UpdateBookModel() { Title = Book.Title };
            command.Model= model;

            //act && assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isime sahip bir kitap zaten mevcut.");

            
           
        }

    }
}
