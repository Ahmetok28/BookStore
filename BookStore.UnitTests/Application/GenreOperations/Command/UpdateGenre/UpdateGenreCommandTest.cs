using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.DBOperations;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookStore.UnitTests.Application.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        // Database de girilen ID'ye ait veri yok ise çalışacak olan kod
        [Fact]
        public void WhenDataNoForId_InvalidOperationExeption_SholudBeReturn()
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
            // Act and Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            var genre = new Genre()
            {
                Name= "Testtttt",
                Id=78907,
                IsActive=true
            };
            _context.Add(genre);
            _context.SaveChanges();
            var klon = genre;
            var cmd = new UpdateGenreCommand(_context, _mapper);
            cmd.GenreId = genre.Id;
            var model = new UpdateGenreModel() { Name = "Cizgi_Roman" };
            cmd.Model = model;

            FluentActions
               .Invoking(() => cmd.Handle()).Invoke();

            var result = _context.Genres.SingleOrDefault(x => x.Name == model.Name);
            result.Should().NotBeNull();
            result.Name.Should().Be(model.Name);
        }

        [Fact]
        public void WhenAlreadyExistGenreTypeIsGiven_InvalidOperationExeption_SholudBeReturn()
        {
            var genre = new Genre()
            {
                Name = "asdffdsg"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            var cmd = new UpdateGenreCommand(_context, _mapper);
            cmd.GenreId = genre.Id;
            var model = new UpdateGenreModel() { Name = "asdffdsg" ,};
            cmd.Model = model;
            FluentActions
                .Invoking(() => cmd.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı İsimde Bir Kitap Türü Zaten var");
        }

    }
}
