using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookStore.UnitTests.Application.AuthorOperations.Commands.UdateAuthor
{
    public class UpdateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        // Database de girilen ID'ye ait veri yok ise çalışacak olan kod
        [Fact]
        public void WhenDataNoForId_InvalidOperationExeption_SholudBeReturn()
        {
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
            // Act and Assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Bu ID'e sahip bir yazar yok");
        }

        //Girilen veriler doğru ise çalışacak olan kod
        [Fact]
        public void WhenValidInputsAreGiven_Authors_ShouldBeUpdated()
        {

            // Arrange
            var authorInDb = new Author
            {
                Name = "WhenGivenAuthorIdExistsInDb",
                SurName = "Author_ShouldBeUpdated",
                BirthDate = new DateTime(1990, 02, 02)
            };
            var authorCompared = new Author
            {
                Name = authorInDb.Name,
                SurName = authorInDb.SurName,
                BirthDate = authorInDb.BirthDate
            };
            _context.Authors.Add(authorInDb);
            _context.SaveChanges();

            var command = new UpdateAuthorCommand(_context, _mapper);
            command.AuthorId = authorInDb.Id;
            command.Model = new UpdateAuthorModel { Name = "UpdatedName", SurName = "UpdatedSurname", BirthDate = new DateTime(1991, 2, 2) };

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var authorUpdated = _context.Authors.SingleOrDefault(b => b.Id == authorInDb.Id);
            authorUpdated.Should().NotBeNull();
            authorUpdated.Name.Should().NotBe(authorCompared.Name);
            authorUpdated.SurName.Should().NotBe(authorCompared.SurName);
            authorUpdated.BirthDate.Should().NotBe(authorCompared.BirthDate);


            //    var author = _context.Authors.SingleOrDefault(x => x.Id == 1);
            //    var klonAuthor = author;
            //    var cmd = new UpdateAuthorCommand(_context, _mapper);
            //    cmd.AuthorId = author.Id;
            //    var model = new UpdateAuthorModel() {Name="allli",SurName="oo",BirthDate = new DateTime(1989, 08, 17) };
            //    cmd.Model= model;

            //    FluentActions
            //        .Invoking(() => cmd.Handle()).Invoke();

            //    var result = _context.Authors.SingleOrDefault(x => x.Id==author.Id);
            //    result.Should().NotBeNull();
            //    result.SurName.Should().Be(model.SurName);
            //    result.Name.Should().Be(model.Name);
            //    result.BirthDate.Should().Be(model.BirthDate);
        }

        // Girilen yazar ismi ile çakışan başka bir yazar ismi varsa çalışacak olan kod
        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationExeption_SholudBeReturn()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == 1);
            var cmd = new UpdateAuthorCommand(_context, _mapper);
            cmd.AuthorId = author.Id;
            var model = new UpdateAuthorModel() { Name = author.Name, SurName = author.SurName };
            cmd.Model = model;
            FluentActions
                .Invoking(() => cmd.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli bir Yazar zaten mevcuttur.");
        }
    }
}
