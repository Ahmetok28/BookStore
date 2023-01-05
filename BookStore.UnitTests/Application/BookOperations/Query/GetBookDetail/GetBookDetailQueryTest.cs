using AutoMapper;
using BookStore.Application.BookOperations.Queries.GetBookDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookStore.Application.BookOperations.Queries.GetBookDetail.GetBookByIdQuery;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookStore.UnitTests.Application.BookOperations.Query.GetBookDetail
{
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvaildInputAreGiven_InvalidOperationExepiton_SholudBeReturn()
        {

            //Arrange
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);

            //Act And Assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu İd' ye Sahip Bir kitap Yok ");
        }

        [Fact]
        public void WhenGivenBookIdExistInDb_Book_ShouldBeReturn()
        {
            //Arrange
            var book = new Book()
            {
                Title = "Testhgjg",
                AuthorId = 1,
                GenreId = 1,
                PageCount = 546,
                PublishDate = new System.DateTime(1999, 09, 07)
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            // var book = _context.Books.SingleOrDefault(x => x.Id == 1);
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.BookId = book.Id;

            //Act
            var result = FluentActions.Invoking(() => query.Handle()).Invoke();

            //Assert
           
            result.Should().NotBeNull();
            result.Id.Should().Be(book.Id);
            result.Title.Should().Be(book.Title);
            result.PageCount.Should().Be(book.PageCount);
            result.PublishDate.Should().Be(book.PublishDate);
        }

    }
}
