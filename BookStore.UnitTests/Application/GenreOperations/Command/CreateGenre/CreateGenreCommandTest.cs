using AutoMapper;

using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;
        public CreateGenreCommandTest(CommonTestFixture testFixture)
        {
            context = testFixture.Context;
            mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreTypeIsGıven_InvalidOperationExeption_ShouldBeReturn()
        {
            var genre = new Genre()
            {
                Name = "hehhh"
            };
            context.Add(genre);
            context.SaveChanges();

           CreateGenreCommand cmd = new CreateGenreCommand(context,mapper);
           cmd.Model= new CreateGenreModel()
            {
                Name = genre.Name
            };

           

            FluentActions
                .Invoking(() => cmd.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü Zaten Mevcut");
        }

        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeCreated()
        {
            CreateGenreCommand cmd = new CreateGenreCommand(context, mapper);
            var Model = new CreateGenreModel() { Name = "Felsefe" };
            cmd.Model = Model;

            FluentActions.Invoking(() => cmd.Handle()).Invoke();

            var result = context.Genres.SingleOrDefault(x=>x.Name== Model.Name);
            result.Should().NotBeNull();
            result.Name.Should().Be(Model.Name);
        }
    }
}
