using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenresDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Query.GetGenresDetail
{
    public class GetGenresDetailQueryTest : IClassFixture<CommonTestFixture>
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;

        }
        [Fact]
        public void WhenInvaildInputAreGiven_InvalidOperationExepiton_SholudBeReturn()
        {

            GetGenresDetailQuery query = new GetGenresDetailQuery(_mapper, _context);
            FluentActions
               .Invoking(() => query.Handle())
               .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");

        }
        [Fact]
        public void WhenGivenBookIdExistInDb_Genre_ShouldBeReturn()
        {
            var genre = new Genre()
            {
                Name = "kjsdhbfkj",
                Id=12343546,
                IsActive= true,
                
            };
            _context.Add(genre);
            _context.SaveChanges();
            GetGenresDetailQuery query = new GetGenresDetailQuery(_mapper,_context);
            query.GenreId = genre.Id;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var result = _context.Genres.SingleOrDefault(x=>x.Id== 12343546 && x.IsActive==true);
            result.Should().NotBeNull();
            result.Name.Should().Be(genre.Name);

        }
    }
}
