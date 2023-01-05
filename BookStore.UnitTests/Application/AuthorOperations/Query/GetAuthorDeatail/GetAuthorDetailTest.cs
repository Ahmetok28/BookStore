using AutoMapper;
using BookStore.Application.AuthorOperations.Query.GetAuthorsDetail;

using BookStore.DBOperations;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.AuthorOperations.Query.GetAuthorDeatail
{
    public class GetAuthorDetailTest : IClassFixture<CommonTestFixture>
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInvaildInputAreGiven_InvalidOperationExepiton_SholudBeReturn()
        {

            //Arrange
        
            GetAuthorsDetailQuery query = new GetAuthorsDetailQuery(_context, _mapper);
            //Act And Assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı");
        }

        [Fact]
        public void WhenGivenBookIdExistInDb_Book_ShouldBeReturn()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == 1);
            GetAuthorsDetailQuery query = new GetAuthorsDetailQuery(_context, _mapper);
            query.AuthorId = author.Id;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var result = _context.Authors.SingleOrDefault(x => x.Id == query.AuthorId);
            result.Should().NotBeNull();
            result.BirthDate.Should().Be(author.BirthDate);
            result.Name.Should().Be(author.Name);   
            result.SurName.Should().Be(author.SurName); 

        }
    }
}
