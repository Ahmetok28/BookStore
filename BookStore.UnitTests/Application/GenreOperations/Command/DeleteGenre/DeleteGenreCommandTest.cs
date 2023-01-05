using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Command.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Fact]
        public void WhenDataNoForIdInDb_InvalidOperationExeption_SholudBeReturn()
        {
            DeleteGenreCommand cmd = new DeleteGenreCommand(_context);

            FluentActions
                .Invoking(() => cmd.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silmek istediğiniz kitap türü DB'de bulunamadı");
        }
        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeDeleted()
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == 1);
            var cmd = new DeleteGenreCommand(_context);
            cmd.GenreId = genre.Id;

            FluentActions.Invoking(() => cmd.Handle()).Invoke();

            var result = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);
            result.Should().BeNull();

        }

    }
}
